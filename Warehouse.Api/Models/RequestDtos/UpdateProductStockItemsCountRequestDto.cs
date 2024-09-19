using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Models.RequestDtos
{
    public class UpdateProductStockItemsCountRequestDto
    {
        public int Id { get; set; }
        public int StockItemsCount { get; set; }
    }
}
