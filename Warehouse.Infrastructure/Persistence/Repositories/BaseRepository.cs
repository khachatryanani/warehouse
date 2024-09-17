using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Warehouse.Infrastructure.Attributes;
using Warehouse.Infrastructure.Persistence.Options;

namespace Warehouse.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T>
    {
        public IMongoCollection<T> Collection { get; init; }
        private readonly IMongoCollection<BsonDocument> _counters;
        private const string _countersCollectionName = "counters";

        public BaseRepository(IOptions<MongoDbOptions> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            var database = client.GetDatabase(options.Value.DatabaseName);
            var collectionName = GetCollectionName();

            Collection = database.GetCollection<T>(collectionName);
            _counters = database.GetCollection<BsonDocument>("counters");
        }

        public int GetNextSequenceValue()
        {
            var collectionName = GetCollectionName();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", collectionName);
            var update = Builders<BsonDocument>.Update.Inc("SeqValue", 1);

            var options = new FindOneAndUpdateOptions<BsonDocument>
            {
                ReturnDocument = ReturnDocument.After,
                IsUpsert = true 
            };

            var result = _counters.FindOneAndUpdate(filter, update, options);
            return result["SeqValue"].AsInt32;
        }

        private static string GetCollectionName()
        {
            var attr = (typeof(T).GetCustomAttributes(typeof(CollectionAttribute), true).FirstOrDefault());
            return (attr as CollectionAttribute)?.Name ?? "unmapped";
        }
    }
}