namespace Warehouse.Api.Models.RequestDtos
{
    public class ProductRequestDto
    {
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int SockItemsCount { get; set; }
    }
}
