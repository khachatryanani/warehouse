namespace Warehouse.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LowStockThreshold { get; set; }
        public int OutOfStockThreshold { get; set; }
    }
}
