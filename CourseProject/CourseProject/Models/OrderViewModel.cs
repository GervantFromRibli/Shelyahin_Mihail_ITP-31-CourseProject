using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string FurnitureName { get; set; }
        public int FurnitureCount { get; set; }
        public decimal Price { get; set; }
        public int DiscountPercent { get; set; }
        public bool IsCompleted { get; set; }
        public string EmployeeFIO { get; set; }
    }
}
