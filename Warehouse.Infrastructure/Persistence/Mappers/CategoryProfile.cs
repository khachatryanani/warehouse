using AutoMapper;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.DataModels;

namespace Warehouse.Infrastructure.Persistence.Mappers
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDataModel>();
            CreateMap<CategoryDataModel, Category>();
        }
    }
}
