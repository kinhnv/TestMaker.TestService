namespace TestMaker.Common.Mongodb
{
    public interface IMongoDbSettings
    {
        string CollectionName { get; set; }

        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
