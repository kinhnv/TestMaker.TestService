namespace TestMaker.Common.Mongodb
{
    public class MongoDbSettings
    {
        private readonly string _connectionString;

        public MongoDbSettings(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string ConnectionString
        {
            get
            {
                var server = _connectionString.Split(";").Single(x => x.Trim().StartsWith("Data Source")).Split("=").Last();
                if (server.IndexOf(",") > 0)
                {
                    return $"mongodb://{server.Split(",")[0]}:{server.Split(",")[1]}";
                }
                return $"mongodb://{server}:27017";
            }
        }
        public string DatabaseName
        {
            get
            {
                return _connectionString.Split(";").Single(x => x.Trim().StartsWith("database")).Split("=").Last();
            }
        }
    }
}
