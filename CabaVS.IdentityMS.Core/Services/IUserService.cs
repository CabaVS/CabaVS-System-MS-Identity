using CabaVS.IdentityMS.Core.Models;
using System.Threading.Tasks;

namespace CabaVS.IdentityMS.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUser(string username, string password, bool isActiveCheck = true);
        Task<bool> IsActive(string username);
    }
}