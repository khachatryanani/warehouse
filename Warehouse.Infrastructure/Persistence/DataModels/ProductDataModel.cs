using Warehouse.Infrastructure.Attributes;

namespace Warehouse.Infrastructure.Persistence.DataModels
{
    [Collection("products")]
    internal class ProductDataModel: BaseDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SockItemsCount { get; set; }
    }
}
