using Identity.API.Helpers;
using Identity.DataAccess;
using Identity.DataAccess.Models.Entities;
using Identity.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System.Security.Claims;

namespace Recipes.API.Controllers;

[ApiController]
public class IdentityController(IdentityDatabaseContext dbContext) : ControllerBase
{
    [HttpGet]
    [Route("users")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllRecipes()
    {
        IEnumerable<UserEntity> users;
        try
        {
            users = await dbContext.Users.ToListAsync();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(users);
    }

    [HttpGet]
    [Route("user/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllRecipes(int id)
    {
        User user;

        try
        {
            UserEntity? userEntity = await dbContext.Users.FindAsync(id);

            if (userEntity is null)
                return NotFound();

            user = new User
            { 
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> RegisterUser(UserCreateRequest userCreateRequest)
    {
        UserEntity userToCreate = new UserEntity()
        {
            Username = userCreateRequest.Username,
            PasswordHash = HashingHelper.HashPassword(userCreateRequest.Password),
            Email = userCreateRequest.Email,
            Roles = UserRole.Member | UserRole.Admin,
        };

        try
        {
            await dbContext.Users.AddAsync(userToCreate);
            await dbContext.SaveChangesAsync();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(JWTHelper.GenerateJwtToken(userToCreate));
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> LoginUser(UserLoginRequest userLoginRequest)
    {
        string jwt;

        try
        {
            UserEntity? user;
            user = await dbContext.Users.Where(u => u.Username.Equals(userLoginRequest.Username)).FirstOrDefaultAsync();
            if (user is null)
                return NotFound();

            if (!user.PasswordHash.SequenceEqual(HashingHelper.HashPassword(userLoginRequest.Password)))
                return Unauthorized();

            jwt = JWTHelper.GenerateJwtToken(user);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(jwt);
    }

    [HttpGet]
    [Authorize]
    [Route("my-account")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetUserInfo()
    {
        int userId = Int32.Parse(HttpContext.User.FindFirstValue("id")!);
        UserEntity user;

        try
        {
            user = (await dbContext.Users.FindAsync(userId))!;
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(user);
    }
}