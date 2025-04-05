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
        private readonly string _subject;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _signKey = _configuration.GetValue<string>("SecretKey");
            _accessTokenExpirationWeb = _configuration.GetValue<string>("AccessTokenExpirationMinutesWeb");
            _subject = _configuration.GetValue<string>("Jwt:Subject");
        }

        public AccessToken Login(User user)
        {
            try
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim("userId", user.Id.ToString()),
                };
                DateTime expirationTime = DateTime.UtcNow.AddMinutes(Double.Parse(_accessTokenExpirationWeb));

                string accessToken = GenerateToken(_signKey, expirationTime, claims);

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
            Claim[] claims = null)
        {
            try
            {
                SecurityKey key = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
                SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                string validIssusers = _configuration.GetValue<string>("ApiUrl");

                var jwtToken = new JwtSecurityToken(
                    validIssusers,
                    _configuration["Jwt:ValidAudience"],
                    claims,
                    expires: utcExpirationTime,
                    signingCredentials: credentials
                    );

                string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
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
