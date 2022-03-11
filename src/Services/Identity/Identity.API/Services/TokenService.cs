using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.BusinessLogic.Interfaces;
using Identity.Infrastructure.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetValue<string>("Jwt:key")));
            _config = config;
        }

        public string CreateToken(User user)
        {
            // adds audience to check
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.NameId, user.Email),
                new(JwtRegisteredClaimNames.Aud, "EmployerAPI"),
                new(JwtRegisteredClaimNames.Aud, "JobSeekerAPI")
            };

            // add role ,roles are JobSeeker and Employer
            claims.Add(new Claim(ClaimTypes.Role, user.UserType.ToString()));


            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config.GetValue<string>("Jwt:Issuer"),
                Audience = null,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}