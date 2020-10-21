using CabaVS.IdentityMS.Core.Models;

namespace CabaVS.IdentityMS.API.Services.Abstractions
{
    public interface ITokenGenerator
    {
        (string AccessToken, string RefreshToken) Generate(User user);
        bool IsValid(string accessToken, bool validateLifetime = true);
    }
}