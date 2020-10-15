using CabaVS.IdentityMS.API.Configuration;
using CabaVS.IdentityMS.API.Services.Abstractions;
using CabaVS.IdentityMS.Core.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CabaVS.IdentityMS.API.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenGenerationConfiguration _tokenGenerationConfig;

        private static DateTime UtcNow => DateTime.UtcNow;
        private double ExpiresInMinutes => _tokenGenerationConfig.ExpiresInMinutes;
        private string Secret => _tokenGenerationConfig.Secret;
        private SymmetricSecurityKey SecurityKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        private SigningCredentials SigningCredentials =>
            new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256Signature);

        public TokenGenerator(IOptions<TokenGenerationConfiguration> tokenGenerationConfig)
        {
            _tokenGenerationConfig = tokenGenerationConfig.Value ?? throw new ArgumentNullException(nameof(tokenGenerationConfig));
        }

        public (string AccessToken, string RefreshToken) Generate(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var now = UtcNow;
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                NotBefore = now,
                Expires = now.AddMinutes(ExpiresInMinutes),
                SigningCredentials = SigningCredentials,
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(token);
            var refreshToken = Guid.NewGuid();

            return (accessToken, refreshToken.ToString());
        }

        public bool IsValid(string accessToken, bool validateLifetime = true)
        {
            if (accessToken == null) throw new ArgumentNullException(nameof(accessToken));

            try
            {
                new JwtSecurityTokenHandler().ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = validateLifetime,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = SecurityKey
                }, out _);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}