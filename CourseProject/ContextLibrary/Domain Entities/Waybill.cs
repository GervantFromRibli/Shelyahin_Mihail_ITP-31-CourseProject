using System;
using System.ComponentModel.DataAnnotations;

namespace ContextLibrary.Domain_Entities
{
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
        public Waybill(int id, int providerId, string provName, DateTime date, string material, decimal price, double weight, int furnitId, int emplId)
        {
            Id = id;
            ProviderId = providerId;
            ProviderName = provName;
            DateOfSupply = date;
            Material = material;
            Price = price;
            Weight = weight;
            FurnitureId = furnitId;
            EmployeeId = emplId;
        }
    }
}
