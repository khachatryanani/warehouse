namespace Warehouse.Domain.Entities
{
    public class StockConfiguration
    {
        public int ProductId { get; set; }
        public int LowStockThreshold { get; set; }
        public int OutOfStockThreshold { get; set; }
    }
}
