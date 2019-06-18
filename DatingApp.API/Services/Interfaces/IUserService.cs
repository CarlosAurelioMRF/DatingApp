using DatingApp.API.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserForDetailedDto> GetUser(string id);
        Task<IEnumerable<UserForListDto>> GetUsers();
    }
}
