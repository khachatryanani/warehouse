namespace Warehouse.Api.Models.RequestDtos
{
    public class CreateProductRequestDto: ProductRequestDto
    {
        public int SockItemsCount { get; set; }
    }
}
