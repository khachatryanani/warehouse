using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.DataModels;
using Warehouse.Infrastructure.Persistence.Options;

namespace Warehouse.Infrastructure.Persistence.Repositories
{
    internal class OrderRepository(IOptions<MongoDbOptions> options, IMapper mapper) : BaseRepository<OrderDataModel>(options), IOrderRepository
    {
        public async Task CreateAsync(Order entity, CancellationToken cancellationToken = default)
        {
            var dataModel = mapper.Map<OrderDataModel>(entity);
            dataModel.CreateDate = DateTime.UtcNow;

            await Collection.InsertOneAsync(dataModel, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Order>> GetAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<OrderDataModel>.Filter.Empty;
            var dataModel = (await Collection.FindAsync(filter, cancellationToken: cancellationToken))
                                             .ToEnumerable(cancellationToken: cancellationToken);

            return mapper.Map<IEnumerable<Order>>(dataModel);
        }

        public async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<OrderDataModel>.Filter.Eq(dm => dm.Id, id);
            var dataModel = (await Collection.FindAsync(filter, cancellationToken: cancellationToken))
                                             .FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<Order>(dataModel);
        }

        public async Task<IEnumerable<Order>> GetByUserIdAsync(int userId, CancellationToken cancellationToken = default)
        {
            var filter = Builders<OrderDataModel>.Filter.Eq(dm => dm.UserId, userId);
            var dataModel = (await Collection.FindAsync(filter, cancellationToken: cancellationToken))
                                             .FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<IEnumerable<Order>>(dataModel);
        }

        public async Task UpdateAsync(int id, Order entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<OrderDataModel>.Filter.Eq(dm => dm.Id, id);

            var update = Builders<OrderDataModel>.Update.Set(x => x.UserId, entity.UserId)
                                                        .Set(x => x.ProductId, entity.ProductId)
                                                        .Set(x => x.ItemsCount, entity.ItemsCount)
                                                        .Set(x => x.UpdateDate, DateTime.UtcNow);

            await Collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }
    }
}
