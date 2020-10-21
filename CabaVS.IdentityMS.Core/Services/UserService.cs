using CabaVS.IdentityMS.Core.Models;
using CabaVS.IdentityMS.Core.Repositories;
using CabaVS.IdentityMS.Core.Services.Abstractions;
using System;
using System.Threading.Tasks;

namespace CabaVS.IdentityMS.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<User> GetUser(string username, string password, bool isActiveCheck = true)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var user = await _userRepository.Get(username, isActiveCheck);
            if (user == null)
            {
                return null;
            }

            var (hashedPassword, _) = _passwordHasher.Hash(password, user.Salt);
            return hashedPassword == user.Password
                ? user
                : null;
        }
    }
}