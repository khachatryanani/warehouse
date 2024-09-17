namespace Warehouse.Api.Models.ResponseDtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ItemsCount { get; set; }
    }
}
