namespace Warehouse.Infrastructure.Persistence.Options
{
    public class MongoDbOptions
    {
        public const string Section = "MongoDbOptions";
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}
