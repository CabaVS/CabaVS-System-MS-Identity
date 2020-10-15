using AutoMapper;
using CabaVS.IdentityMS.Core.Models;
using CabaVS.IdentityMS.Infrastructure.Entities;

namespace CabaVS.IdentityMS.Infrastructure.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, User>();
            CreateMap<User, UserEntity>();
        }
    }
}