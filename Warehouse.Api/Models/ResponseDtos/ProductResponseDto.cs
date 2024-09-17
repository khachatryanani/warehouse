namespace Warehouse.Api.Models.ResponseDtos
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SockItemsCount { get; set; }
    }
}
