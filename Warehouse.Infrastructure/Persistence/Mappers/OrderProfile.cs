using AutoMapper;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Persistence.DataModels;

namespace Warehouse.Infrastructure.Persistence.Mappers
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDataModel>();
            CreateMap<OrderDataModel, Order>();
        }
    }
}
