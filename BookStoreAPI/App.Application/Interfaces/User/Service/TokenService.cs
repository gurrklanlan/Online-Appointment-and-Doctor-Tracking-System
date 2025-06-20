using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using App.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.Application.Interfaces.User.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TokenResponse CreateToken(Domain.Entites.User user)
        {

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var keystring = _configuration["Jwt:Key"];
            if(string.IsNullOrEmpty(keystring))
              throw new InvalidOperationException("JWT Key is not configured in the appsettings.json file.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keystring));

            var token = new JwtSecurityToken
                ( 
                 issuer: _configuration["Jwt:Issuer"],
                 audience: _configuration["Jwt:Audience"],
                 claims: authClaims,
                 expires: DateTime.Now.AddMinutes(30),
                 signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)

                );

           return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshtTokenExtTime = token.ValidTo,
                RefreshToken = GenerateRefreshToken()
            };

        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
