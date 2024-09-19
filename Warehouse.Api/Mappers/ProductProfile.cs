using AutoMapper;
using Warehouse.Api.Models.RequestDtos;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Mappers
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductRequestDto, Product>();
            CreateMap<CreateProductRequestDto, Product>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}
