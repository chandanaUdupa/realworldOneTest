using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using realworldOneTest.Data;
using realworldOneTest.Entities;
using realworldOneTest.Utility;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace realworldOneTest.Authentication
{
    public class JWTAuthenticationHandler
    {
        private readonly UserContext _context;
        private readonly ILogger _log;

        public JWTAuthenticationHandler(
            ILogger<JWTAuthenticationHandler> logger)
        {
            _log = logger;
        }

        public string Authenticate(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(WebAppSettings.GetSecretKey());
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return token.ToString();
        }
    }
}
