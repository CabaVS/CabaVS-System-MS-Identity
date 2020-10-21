namespace CabaVS.IdentityMS.Core.Services.Abstractions
{
    public interface IPasswordHasher
    {
        (string HashedPassword, string Salt) Hash(string password, string salt = null);
    }
}