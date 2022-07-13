using MongoDB.Driver;

namespace TestMaker.Common.Mongodb
{
    public class MongoContext : IMongoContext
    {
        public MongoContext(MongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            Database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoDatabase Database { get; }
    }
}
