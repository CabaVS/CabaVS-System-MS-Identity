using CabaVS.IdentityMS.Core.Services.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace CabaVS.IdentityMS.API.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public (string HashedPassword, string Salt) Hash(string password, string salt = null)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            salt ??= GenerateSalt();

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA1,
                10000,
                256 / 8));
            return (hashed, salt);
        }

        private static string GenerateSalt()
        {
            var salt = new byte[128 / 8];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            var convertedSalt = Convert.ToBase64String(salt);
            return convertedSalt;
        }
    }
}