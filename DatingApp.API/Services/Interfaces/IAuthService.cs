using DatingApp.API.Dtos;
using DatingApp.API.Models.UserAgg;
using System.Threading.Tasks;

namespace DatingApp.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> Register(UserForRegisterDto userForRegisterDto);
        Task<string> Login(UserForLoginDto userForLogin);
    }
}
