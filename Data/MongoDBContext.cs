using E_Commerce.Models;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace E_Commerce.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IConfiguration configuration)
        {
            // Configuration retrieves the appsettings.json fields
            var connectionString = configuration.GetConnectionString("MongoDB");
            var client = new MongoClient(connectionString);
            // This name comes from the Users Database that we created to add Users into the project
            _database = client.GetDatabase("Users");

        }
        // This is then how we retrieve the colleciton of Users
        public IMongoCollection<E_Commerce.Models.User> Users => _database.GetCollection<E_Commerce.Models.User>("Users")

    }
}
