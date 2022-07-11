using MongoDB.Driver;

namespace TestMaker.Common.Mongodb
{
    public class MongoRepository<T> : IMongoRepository<T> where T : MongoEntity
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection _collection;

        public MongoRepository(string connectionString, string database)
        {
            MongoClient client = new MongoClient(connectionString);
            //Dùng lệnh GetDatabase để kết nối Cơ sở dữ liệu
            _database = client.GetDatabase("MainDatabase");
            _collection = _database.GetCollection<T>("MainCollection").Find(x);
        }
    }
}
