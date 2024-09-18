using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.DataModels;
using Warehouse.Infrastructure.Persistence.Options;

namespace Warehouse.Infrastructure.Persistence.Repositories
{
    internal class CategoryRepository(IOptions<MongoDbOptions> options, IMapper mapper) : BaseRepository<CategoryDataModel>(options), ICategoryRepository
    {
        public async Task<int> CreateAsync(Category entity, CancellationToken cancellationToken = default)
        {
            var dataModel = mapper.Map<CategoryDataModel>(entity);
            dataModel.CreateDate = DateTime.UtcNow;
            dataModel.Id = GetNextSequenceValue();

            await Collection.InsertOneAsync(dataModel, cancellationToken: cancellationToken);
            return dataModel.Id;
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<CategoryDataModel>.Filter.Eq(dm => dm.Id, id);

            await Collection.DeleteOneAsync(filter, cancellationToken: cancellationToken);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<CategoryDataModel>.Filter.Eq(dm => dm.Id, id);

            return await Collection.Find(filter).AnyAsync(cancellationToken);
        }

        public async Task<IEnumerable<Category>> GetAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<CategoryDataModel>.Filter.Empty;
            var dataModel = (await Collection.FindAsync(filter, cancellationToken: cancellationToken))
                                             .ToEnumerable(cancellationToken: cancellationToken);

            return mapper.Map<IEnumerable<Category>>(dataModel);
        }

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<CategoryDataModel>.Filter.Eq(dm => dm.Id, id);
            var dataModel = await Collection.Find(filter)
                                            .FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<Category>(dataModel);
        }

        public async Task UpdateAsync(int id, Category entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<CategoryDataModel>.Filter.Eq(dm => dm.Id, id);

            var update = Builders<CategoryDataModel>.Update.Set(x => x.Name, entity.Name)
                                                          .Set(x => x.OutOfStockThreshold, entity.OutOfStockThreshold)
                                                          .Set(x => x.LowStockThreshold, entity.LowStockThreshold)
                                                          .Set(x => x.UpdateDate, DateTime.UtcNow);

            await Collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }
    }
}
