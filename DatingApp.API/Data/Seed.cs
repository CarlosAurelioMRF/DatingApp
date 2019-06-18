using DatingApp.API.Models.UserAgg;
using DatingApp.API.Models.UserAgg.Repository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class Seed
    {
        private readonly IAuthRepository _repository;

        public Seed(IAuthRepository repository)
        {
            _repository = repository;
        }

        public async Task SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            foreach (var user in users)
            {
                user.Username = user.Username.ToLower();

                await _repository.Register(user, "password");
            }
        }
    }
}
