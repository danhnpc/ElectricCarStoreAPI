using ElectricCarStore_DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ElectricCarStore_BLL.Service
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        private readonly string _signKey;
        private readonly string _accessTokenExpirationWeb;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _signKey = _configuration.GetValue<string>("SecretKey");
            _accessTokenExpirationWeb = _configuration.GetValue<string>("AccessTokenExpirationMinutesWeb");
        }

        public AccessToken Login(User user)
        {
            try
            {
                var claim = new ClaimsIdentity(new List<Claim> {
                        new Claim("userId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Admin")
                    });
                DateTime expirationTime = DateTime.UtcNow.AddMinutes(Double.Parse(_accessTokenExpirationWeb));

                string accessToken = GenerateToken(_signKey, expirationTime, claim);

                return new AccessToken
                {
                    Value = accessToken,
                    ExpirationTime = expirationTime,
                    RefreshToken = null
                };
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source: {0}", e.Message);
            }
            return new AccessToken();
        }

        public string GenerateToken(string secretKey, DateTime utcExpirationTime,
            ClaimsIdentity claims = null)
        {
            try
            {
                byte[] keyBytes = Convert.FromBase64String(secretKey);
                Console.WriteLine($"Key Length: {keyBytes.Length * 8} bits"); // Kiểm tra số bit
                var length = keyBytes.Length * 8;
                SecurityKey key = new SymmetricSecurityKey(System.Convert.FromBase64String(secretKey));
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                string validIssusers = _configuration.GetValue<string>("ApiUrl");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = validIssusers,              // Not required as no third-party is involved
                    Audience = null,            // Not required as no third-party is involved
                    IssuedAt = DateTime.UtcNow,
                    NotBefore = DateTime.UtcNow,
                    Expires = utcExpirationTime,
                    Subject = claims,
                    SigningCredentials = credentials
                };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
                var token = jwtTokenHandler.WriteToken(jwtToken);
                return token;
            }
            catch (Exception e)
            {
                if (e.Source != null)
                    Console.WriteLine("Exception source: {0}", e.Message);
                return "Error generate token";

            }
        }
    }

    public class AccessToken
    {
        public string Value { get; set; }
        public DateTime ExpirationTime { get; set; }
        public string RefreshToken { get; set; }
    }
}
