using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapi.BLL.Models;

namespace webapi.BLL.Helpers
{
    public class JwtCreator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtCreator(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateJwtToken(string userEmail, string userRole)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userEmail),
            };

            if (!string.IsNullOrEmpty(userRole))
            {
                claims = claims.Append(new Claim(ClaimTypes.Role, userRole)).ToArray();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
