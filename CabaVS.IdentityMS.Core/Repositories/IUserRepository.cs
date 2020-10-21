using CabaVS.IdentityMS.Core.Models;
using System.Threading.Tasks;

namespace CabaVS.IdentityMS.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> Get(string username, bool isActiveCheck = true);
    }
}