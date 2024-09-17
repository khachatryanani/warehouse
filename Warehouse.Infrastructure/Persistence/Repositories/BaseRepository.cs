using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Warehouse.Infrastructure.Attributes;
using Warehouse.Infrastructure.Persistence.Options;

namespace Warehouse.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T>
    {
        public IMongoCollection<T> Collection { get; init; }

        public BaseRepository(IOptions<MongoDbOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            var collectionName = GetCollectionName();

            Collection = database.GetCollection<T>(collectionName);
        }

        private static string GetCollectionName()
        {
            var attr = (typeof(T).GetCustomAttributes(typeof(CollectionAttribute), true).FirstOrDefault());
            return (attr as CollectionAttribute)?.Name ?? "unmapped";
        }
    }
}