using AutoMapper;
using Warehouse.Api.Models.RequestDtos;
using Warehouse.Api.Models.ResponseDtos;
using Warehouse.Domain.Entities;

namespace Warehouse.Api.Mappers
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderRequestDto, Order>();
            CreateMap<Order, OrderResponseDto>();
        }
    }
}
