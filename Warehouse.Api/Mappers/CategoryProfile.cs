using AutoMapper;
using Warehouse.Api.Models.RequestDtos;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Mappers
{
    public class CategoryProfile: Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
