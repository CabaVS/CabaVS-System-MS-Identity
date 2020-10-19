namespace CabaVS.IdentityMS.Core.Helpers
{
    public interface IPasswordHasher
    {
        (string HashedPassword, string Salt) Hash(string password, string salt = null);
    }
}