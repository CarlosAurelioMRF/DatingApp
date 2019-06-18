using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models.UserAgg.Repository;
using DatingApp.API.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserForDetailedDto> GetUser(string id) => _mapper.Map<UserForDetailedDto>(await _repository.GetUser(id));

        public async Task<IEnumerable<UserForListDto>> GetUsers() => _mapper.Map<IEnumerable<UserForListDto>>(await _repository.GetUsers());
    }
}
