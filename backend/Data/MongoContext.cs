using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using backend.Models.Entities;

namespace backend.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            var connectionString = configuration["MongoDbSettings:ConnectionString"];
            var databaseName = configuration["MongoDbSettings:DatabaseName"];

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("MongoDB connection string is missing.");

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<Fest> Fests =>
            _database.GetCollection<Fest>("fests");

        public IMongoCollection<Ticket> Tickets =>
            _database.GetCollection<Ticket>("tickets");

        public IMongoCollection<MealSlot> MealSlots =>
            _database.GetCollection<MealSlot>("mealSlots");
    
        public IMongoCollection<ScanLog> ScanLogs =>
            _database.GetCollection<ScanLog>("scanLogs");

        public IMongoCollection<MealConsumption> MealConsumptions =>
            _database.GetCollection<MealConsumption>("mealConsumptions");

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("users");
    }

}
