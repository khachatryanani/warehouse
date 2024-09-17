using AutoMapper;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.DataModels;

namespace Warehouse.Infrastructure.Persistence.Mappers
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDataModel>();
            CreateMap<ProductDataModel, Product>();
        }
    }
}
