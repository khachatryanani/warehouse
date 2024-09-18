namespace Warehouse.Common.Contracts.Messages
{
    public class ReviewOrderRequest
    {
        public Guid CorrelationId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ItemsCount { get; set; }
    }
}
