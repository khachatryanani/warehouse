namespace Warehouse.Api.Models.RequestDtos
{
    public class CategoryRequestDto
    {
        public string Name { get; set; }
        public int LowStockThreshold { get; set; }
        public int OutOfStockThreshold { get; set; }
    }
}
