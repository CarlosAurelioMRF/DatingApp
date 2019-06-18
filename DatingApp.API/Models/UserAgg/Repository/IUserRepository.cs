using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.Models.UserAgg.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUser(string id);
        Task<IEnumerable<User>> GetUsers();
    }
}
