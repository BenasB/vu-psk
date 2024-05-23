using System.Text;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using Identity.DataAccess.Models.Entities;
using System.Text.Json;
using Identity.API.Options;
using Microsoft.Extensions.Options;

namespace Identity.API.Helpers;

public class JWTHelper
{
    private readonly JWTOptions _jwtOptions;

    public JWTHelper(IOptions<JWTOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string GenerateJwtToken(UserEntity user)
    {
        DateTime value = DateTime.Now.AddMinutes(20.0);
        byte[] bytes = Encoding.UTF8.GetBytes(_jwtOptions.Key);
        SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256);

        Dictionary<string, object> claims = new Dictionary<string, object> { { "id", user.Id.ToString() }, { "username", user.Username }, { "roles", JsonSerializer.Serialize(user.Roles) } };

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Expires = value,
            SigningCredentials = signingCredentials,
            Claims = claims
        };
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return jwtSecurityTokenHandler.WriteToken(token);
    }
}
