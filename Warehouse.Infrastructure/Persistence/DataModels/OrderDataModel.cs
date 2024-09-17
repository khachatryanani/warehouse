using Warehouse.Infrastructure.Attributes;

namespace Warehouse.Infrastructure.Persistence.DataModels
{
    [Collection("orders")]

    internal class OrderDataModel: BaseDataModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ItemsCount { get; set; }
    }
}
