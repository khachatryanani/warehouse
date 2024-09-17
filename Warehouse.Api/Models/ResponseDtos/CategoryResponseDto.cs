namespace Warehouse.Api.Models.ResponseDtos
{
    public class CategoryResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LowStockThreshold { get; set; }
        public int OutOfStockThreshold { get; set; }
    }
}
