using AutoMapper;
using CabaVS.IdentityMS.Core.Models;
using CabaVS.IdentityMS.Core.Repositories;
using CabaVS.IdentityMS.Infrastructure.Entities;
using CabaVS.Shared.Abstractions.Repository;
using CabaVS.Shared.Abstractions.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace CabaVS.IdentityMS.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private IRepository<UserEntity> Repository => _unitOfWork.GetRepository<UserEntity>();

        public UserRepository(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<User> Get(string username, bool isActiveCheck = true)
        {
            if (username == null) throw new ArgumentNullException(nameof(username));

            var userEntity = await Repository.GetFirstAsync(predicate: x =>
                x.Username == username && (!isActiveCheck || !x.IsBlocked));
            if (userEntity == null)
            {
                return null;
            }

            var user = _mapper.Map<UserEntity, User>(userEntity);
            return user;
        }
    }
}