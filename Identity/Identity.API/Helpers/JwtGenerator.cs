using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Identity.DataAccess.Models.Entities;
using Identity.API.Options;
using Microsoft.Extensions.Options;

namespace Identity.API.Helpers;

public class JwtGenerator(IOptions<JWTOptions> jwtOptions)
{
    private readonly JWTOptions _jwtOptions = jwtOptions.Value;

    public string GenerateJwtToken(UserEntity user)
    {
        var value = DateTime.Now.AddMinutes(20.0);
        var bytes = Encoding.UTF8.GetBytes(_jwtOptions.Key);
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256);

        var claims = new Dictionary<string, object>
        {
            { ClaimTypes.NameIdentifier, user.Id },
            { ClaimTypes.Name, user.Username },
            { ClaimTypes.Role, user.Roles },
            { ClaimTypes.Email, user.Email },
            { "imageUrl", $"https://api.dicebear.com/8.x/thumbs/svg?seed={user.Username}"}
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Expires = value,
            SigningCredentials = signingCredentials,
            Claims = claims
        };
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return jwtSecurityTokenHandler.WriteToken(token);
    }
}
