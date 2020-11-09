using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int FurnitureId { get; set; }
        public int FurnitureCount { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public int IsCompleted { get; set; }
        public int EmployeeId { get; set; }
        public Order() { }
    }
}
