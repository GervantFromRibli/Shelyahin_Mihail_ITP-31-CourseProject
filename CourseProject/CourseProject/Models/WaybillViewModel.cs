using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class WaybillViewModel
    {
        public int Id { get; set; }
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public DateTime DateOfSupply { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public double Weight { get; set; }
        public string FurnitureName { get; set; }
        public string EmployeeFIO { get; set; }
    }
}
