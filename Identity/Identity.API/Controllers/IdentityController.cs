using Identity.API.Helpers;
using Identity.DataAccess;
using Identity.DataAccess.Models.Entities;
using Identity.Public;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Recipes.API.Controllers;

[ApiController]
public class IdentityController(IdentityDatabaseContext dbContext) : ControllerBase
{
    [Route("users")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAllRecipes()
    {
        IEnumerable<UserEntity> users;
        try
        {
            users = await dbContext.Set<UserEntity>().ToListAsync();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(users);
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
            Roles = UserRole.Member,
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

        return StatusCode(StatusCodes.Status201Created);
    }

    [Route("login")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<UserEntity>>> LoginUser(UserLoginRequest userLoginRequest)
    {
        UserEntity? user;

        try
        {
            user = await dbContext.Users.Where(u => u.Username.Equals(userLoginRequest.Username)).FirstOrDefaultAsync();
            if (user is null)
                return StatusCode(StatusCodes.Status404NotFound);

            if (!user.PasswordHash.SequenceEqual(HashingHelper.HashPassword(userLoginRequest.Password)))
                return StatusCode(StatusCodes.Status401Unauthorized);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return StatusCode(StatusCodes.Status201Created, user);
    }
}