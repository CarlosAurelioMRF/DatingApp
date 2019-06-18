using DatingApp.API.Models;
using DatingApp.API.Models.UserAgg;
using DatingApp.API.Models.UserAgg.Repository;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class UserRepository : IUserRepository
    {
        #region Members
        private readonly IMongoCollection<User> _users;
        #endregion

        #region Constructor
        public UserRepository(IDatingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(nameof(User));
        }
        #endregion   

        public async Task<User> GetUser(string id) => await _users.Find(c => c.Id == id).FirstOrDefaultAsync();

        public async Task<IEnumerable<User>> GetUsers() => await _users.Find(_ => true).ToListAsync();
    }
}
