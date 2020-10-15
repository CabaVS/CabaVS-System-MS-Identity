using AutoMapper;
using CabaVS.IdentityMS.Core.Models;
using CabaVS.IdentityMS.Core.Services;
using CabaVS.IdentityMS.Infrastructure.Entities;
using CabaVS.Shared.Abstractions.Repository;
using CabaVS.Shared.Abstractions.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace CabaVS.IdentityMS.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<UserEntity> UserRepository => _unitOfWork.GetRepository<UserEntity>();

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<User> GetUser(string username, string password, bool isActiveCheck = true)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));
            if (password == null) throw new ArgumentNullException(nameof(password));

            var userEntity = await UserRepository.GetFirstAsync(predicate: x =>
                x.Username == username && x.Password == password && (!isActiveCheck || !x.IsBlocked));
            if (userEntity == null)
            {
                return null;
            }

            var user = _mapper.Map<UserEntity, User>(userEntity);
            return user;
        }

        public Task<bool> IsActive(string username)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            return UserRepository.IsAnyAsync(x => x.Username == username && !x.IsBlocked);
        }
    }
}