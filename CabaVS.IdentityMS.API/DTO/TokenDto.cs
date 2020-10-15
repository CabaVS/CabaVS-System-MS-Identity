using CabaVS.IdentityMS.Core.Models;
using System;

namespace CabaVS.IdentityMS.API.DTO
{
    public class TokenDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; }

        public TokenDto(User user, string accessToken, string refreshToken)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Username = user.Username;
            UserId = user.Id;
        }

        public TokenDto(User user, (string AccessToken, string RefreshToken) tokens)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            var (accessToken, refreshToken) = tokens;

            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Username = user.Username;
            UserId = user.Id;
        }
    }
}