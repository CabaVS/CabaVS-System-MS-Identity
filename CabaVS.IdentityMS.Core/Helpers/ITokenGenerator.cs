using CabaVS.IdentityMS.Core.Models;

namespace CabaVS.IdentityMS.Core.Helpers
{
    public interface ITokenGenerator
    {
        (string AccessToken, string RefreshToken) Generate(User user);
        bool IsValid(string accessToken, bool validateLifetime = true);
    }
}