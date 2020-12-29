using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab7.Models
{
    // Модель, представляющая запись в таблице Накладные
    public class Waybill
    {
        [Key]
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public DateTime DateOfSupply { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
        [ForeignKey("Furniture")]
        public int FurnitureId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Waybill() { }
    }
}
