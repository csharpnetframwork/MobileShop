using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class StockItem
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Specification { get; set; }
        public string? Rate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? ModifyBy { get; set; }
        public string ModifyDate { get; set; }=DateTime.Now.ToString();
        public bool iteminstock { get; set; }

    }
}
