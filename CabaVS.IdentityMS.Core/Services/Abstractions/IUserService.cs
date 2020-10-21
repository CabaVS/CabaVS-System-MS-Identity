using CabaVS.IdentityMS.Core.Models;
using System.Threading.Tasks;

namespace CabaVS.IdentityMS.Core.Services.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUser(string username, string password, bool isActiveCheck = true);
    }
}