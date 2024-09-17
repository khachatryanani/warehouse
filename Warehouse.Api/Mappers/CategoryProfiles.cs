using AutoMapper;
using Warehouse.Api.Models.RequestDtos;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Mappers
{
    public class CategoryProfiles: Profile
    {
        public CategoryProfiles()
        {
            CreateMap<CategoryRequestDto, Category>();
            CreateMap<Category, CategoryResponseDto>();
        }
    }
}
