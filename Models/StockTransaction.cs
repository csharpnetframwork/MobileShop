namespace MobileShop.Models
{
    public class StockTransaction
    {
        public int Id { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PerformedBy { get; set; }
    }
}
