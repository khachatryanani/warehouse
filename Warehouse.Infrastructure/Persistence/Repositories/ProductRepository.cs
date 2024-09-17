﻿using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Warehouse.Domain.Abstractions;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.DataModels;
using Warehouse.Infrastructure.Persistence.Options;

namespace Warehouse.Infrastructure.Persistence.Repositories
{
    internal class ProductRepository(IOptions<MongoDbOptions> options, IMapper mapper) : BaseRepository<ProductDataModel>(options), IProductRepository
    {
        public async Task CreateAsync(Product entity, CancellationToken cancellationToken = default)
        {
            var dataModel = mapper.Map<ProductDataModel>(entity);
            dataModel.CreateDate = DateTime.UtcNow;
            dataModel.Id = GetNextSequenceValue();

            await Collection.InsertOneAsync(dataModel, cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken = default)
        {
            var filter = Builders<ProductDataModel>.Filter.Empty;
            var dataModel = (await Collection.FindAsync(filter, cancellationToken: cancellationToken))
                                             .ToEnumerable(cancellationToken: cancellationToken);

            return mapper.Map<IEnumerable<Product>>(dataModel);
        }

        public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var filter = Builders<ProductDataModel>.Filter.Eq(dm => dm.Id, id);
            var dataModel = await Collection.Find(filter)
                                            .FirstOrDefaultAsync(cancellationToken);

            return mapper.Map<Product>(dataModel);
        }

        public async Task UpdateAsync(int id, Product entity, CancellationToken cancellationToken = default)
        {
            var filter = Builders<ProductDataModel>.Filter.Eq(dm => dm.Id, id);

            var update = Builders<ProductDataModel>.Update.Set(x => x.Name, entity.Name)
                                                          .Set(x => x.CategoryId, entity.CategoryId)
                                                          .Set(x => x.SockItemsCount, entity.SockItemsCount)
                                                          .Set(x => x.UpdateDate, DateTime.UtcNow);
            
            await Collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }
    }
}
