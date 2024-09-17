using Warehouse.Infrastructure.Attributes;

namespace Warehouse.Infrastructure.Persistence.DataModels
{
    [Collection("categories")]
    internal class CategoryDataModel: BaseDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LowStockThreshold { get; set; }
        public int OutOfStockThreshold { get; set; }
    }
}
