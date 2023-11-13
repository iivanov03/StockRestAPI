using Microsoft.IdentityModel.Tokens;

using StockRestApi.Auth.Services.Contracts;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockRestApi.Auth.Services
{
    public class AuthService : IAuthService
    {
        public string GenerateToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKeyHere")); // replace with your secret key
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
