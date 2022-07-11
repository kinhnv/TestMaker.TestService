using MongoDB.Driver;

namespace TestMaker.Common.Mongodb
{
    public interface IMongoContext
    {
        IMongoDatabase Database { get; }
    }
}
