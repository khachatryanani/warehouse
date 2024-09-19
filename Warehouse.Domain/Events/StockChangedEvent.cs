namespace Warehouse.Domain.Events
{
    public class StockChangedEvent
    {
        public int ProductId { get; set; }

        public int StockItemsCount { get; set; }
    }
}
