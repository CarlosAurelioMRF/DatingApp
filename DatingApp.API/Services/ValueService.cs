using DatingApp.API.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace DatingApp.API.Services
{
    public class ValueService
    {
        private readonly IMongoCollection<Value> _values;

        public ValueService(IDatingDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _values = database.GetCollection<Value>(nameof(Value));
        }

        public List<Value> Get() =>
            _values.Find(book => true).ToList();

        public Value Get(string id) =>
            _values.Find(book => book.Id == id).FirstOrDefault();

        public Value Create(Value book)
        {
            _values.InsertOne(book);
            return book;
        }

        public void Update(string id, Value bookIn) =>
            _values.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Value bookIn) =>
            _values.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _values.DeleteOne(book => book.Id == id);
    }
}
