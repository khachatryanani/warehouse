using Microsoft.AspNetCore.Mvc;

namespace Warehouse.Api.Models.RequestDtos
{
    public class UpdateProductStockItemsCountRequestDto
    {
        [FromRoute]
        public int Id { get; set; }
        [FromBody]
        public int StockItemsCount { get; set; }
    }
}
