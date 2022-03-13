using Application.Models.Login;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Application.Services.Implementation
{
    public class JwtService
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly SymmetricSecurityKey _securityKey;
        private readonly SigningCredentials _credentials;

        public JwtService()
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("digitclassifier11111"));
            _credentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);
        }
        public string GenerateJwt(LoginRequest userInfo)
        {

            var claims = new []
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(ClaimsIdentity.DefaultNameClaimType, userInfo.UserName),
            };

            var token = new JwtSecurityToken
            (
                ".net",
                audience: "commonUsers",
                claims,
                DateTime.UtcNow.AddMilliseconds(-30),
                DateTime.UtcNow.AddDays(7),
                _credentials
            );

            return _jwtSecurityTokenHandler.WriteToken(token);
        }
        public string ValidateToken(string token)
        {
            if (token == null)
                return null;

            try
            {
                _jwtSecurityTokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = _securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userName = jwtToken.Claims.First(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value;

                return userName;
            }
            catch
            {
                return null;
            }
        }
    }
}
