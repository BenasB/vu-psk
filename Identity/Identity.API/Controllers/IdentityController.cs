using Identity.API.Helpers;
using Identity.DataAccess;
using Identity.DataAccess.Models;
using Identity.DataAccess.Models.Entities;
using Identity.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Security.Claims;

namespace Identity.API.Controllers;

[ApiController]
public class IdentityController(IdentityDatabaseContext dbContext, JwtGenerator jwtGenerator) : ControllerBase
{
    [HttpGet]
    [Route("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
    {
        IEnumerable<User> users;
        try
        {
            users = (await dbContext.Users.ToListAsync())
                .Select(u => new User() { Id = u.Id,     Username = u.Username, Email = u.Email });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(users);
    }

    [HttpGet]
    [ActionName("GetUser")]
    [Route("user/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        User user;  

        try
        {
            UserEntity? userEntity = await dbContext.Users.FindAsync(id);
            if (userEntity is null)
                return NotFound();

            user = new User
            { 
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email
            };
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(user);
    }

    [Route("register")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> RegisterUser(UserCreateRequest userCreateRequest)
    {
        UserEntity userToCreate = new UserEntity()
        {
            Username = userCreateRequest.Username,
            PasswordHash = HashingHelper.HashPassword(userCreateRequest.Password),
            Email = userCreateRequest.Email,
            Roles = new List<string> { UserRoles.ADMIN, UserRoles.MEMBER },
        };

        try
        {
            await dbContext.Users.AddAsync(userToCreate);
            await dbContext.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when ((ex.InnerException as PostgresException)?.SqlState == "23505")
        { 
            return Conflict();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction("GetUser", new { id = userToCreate.Id }, jwtGenerator.GenerateJwtToken(userToCreate));
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<string>> LoginUser(UserLoginRequest userLoginRequest)
    {
        string jwt;

        try
        {
            UserEntity? user = await dbContext.Users.Where(u => u.Username.Equals(userLoginRequest.Username)).FirstOrDefaultAsync();
            if (user is null)
                return NotFound();

            if (!user.PasswordHash.SequenceEqual(HashingHelper.HashPassword(userLoginRequest.Password)))
                return Unauthorized();

            jwt = jwtGenerator.GenerateJwtToken(user);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(jwt);
    }

    [HttpGet]
    [Authorize]
    [Route("my-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<User>> GetMyAccountInfo()
    {
        int userId = Int32.Parse(HttpContext.User.FindFirstValue("id")!);
        User user;

        try
        {
            UserEntity? userEntity = (await dbContext.Users.FindAsync(userId))!;
            if(userEntity is null)
                return NotFound();

            user = new User()
            {
                Id = userEntity.Id,
                Username = userEntity.Username,
                Email = userEntity.Email,
            };
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(user);
    }
}