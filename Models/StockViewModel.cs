namespace Shop.Models
{
    public class StockViewModel
    {
        public IEnumerable<StockItem> StockItems { get; set; }
          public StockItem NewStockItem { get; set; }
    }
}
