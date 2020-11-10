using System;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
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
        public int FurnitureId { get; set; }
        public int EmployeeId { get; set; }
        public Waybill() { }
    }
}
