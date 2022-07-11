namespace TestMaker.Common.Mongodb
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string CollectionName { get; set; } = "MainCollection";

        public string ConnectionString { get; set; } = string.Empty;

        public string DatabaseName { get; set; } = "MainCollection";
    }
}
