namespace MobileShop.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
        public int Quantity { get; set; }
        public DateTime SaleDate { get; set; }
        public string SoldBy { get; set; }
    }
}
