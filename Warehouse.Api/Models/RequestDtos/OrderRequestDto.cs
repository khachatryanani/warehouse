namespace Warehouse.Api.Models.RequestDtos
{
    public class OrderRequestDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ItemsCount { get; set; }
    }
}
