using DatingApp.API.Models;
using DatingApp.API.Models.UserAgg;
using DatingApp.API.Models.UserAgg.Repository;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace DatingApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        #region Members
        private readonly IMongoCollection<User> _users;
        #endregion

        #region Constructor
        public AuthRepository(IDatingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(nameof(User));
        }
        #endregion      

        public async Task<User> Login(string username, string password)
        {           
            var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _users.InsertOneAsync(user);

            return user;
        }

        public async Task<bool> UserExists(string username)
        {
            return await _users.Find(c => c.Username == username).AnyAsync();
        }

        #region Private Methods
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != passwordHash[i]) return false;
            }

            return true;
        }
        #endregion
    }
}
