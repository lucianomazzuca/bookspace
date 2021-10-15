using bookspace.Api.Entities;
using bookspace.Api.UOW;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace bookspace.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(UnitOfWork unitOfWork, IConfiguration configuration)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public bool VerifyPassword(string hash, string password)
        {
            var pass = CryptographyService.GetSha256(password);
            return hash == pass;
        }

        public string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig").GetSection("secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
