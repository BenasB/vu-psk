using Identity.DataAccess.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Identity.API.Helpers;

public class JWTHelper
{
    public static string GenerateJwtToken(UserEntity user)
    {
        DateTime value = DateTime.Now.AddMinutes(20.0);
        byte[] bytes = Encoding.UTF8.GetBytes(ApplicationSettingsHelper.Settings.GetSection("JWTSettings:Key").Value!);
        SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(bytes), SecurityAlgorithms.HmacSha256);

        Dictionary<string, object> claims = new Dictionary<string, object> { { "id", user.Id.ToString() }, { "roles", ((int)user.Roles).ToString() } };

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = ApplicationSettingsHelper.Settings.GetSection("JWTSettings:Issuer").Value,
            Audience = ApplicationSettingsHelper.Settings.GetSection("JWTSettings:Audience").Value,
            Expires = value,
            SigningCredentials = signingCredentials,
            Claims = claims
        };
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = jwtSecurityTokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return jwtSecurityTokenHandler.WriteToken(token);
    }
}
