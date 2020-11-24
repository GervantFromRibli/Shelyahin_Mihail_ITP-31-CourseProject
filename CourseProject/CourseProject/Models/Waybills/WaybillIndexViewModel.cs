using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class WaybillIndexViewModel
    {
        public List<WaybillViewModel> WaybillViewModels { get; set; }

        public List<string> EmployeesFIOs { get; set; }

        public List<string> FurnitureNames { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public List<string> FilterFurnitureNames { get; set; }

        public string FilterFurnitName { get; set; }

        public string FilterProviderName { get; set; }

        public List<int> Ids { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Номер поставщика")]
        public int ProviderId { get; set; }

        [Display(Name = "Название мебели")]
        public string FurnitureName { get; set; }

        [Display(Name = "Название поставщика")]
        public string ProviderName { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Price { get; set; }

        [Display(Name = "Дата поставок")]
        public DateTime DateOfSupply { get; set; }

        [Display(Name = "Материал")]
        public string Material { get; set; }

        [Display(Name = "ФИО работника")]
        public string EmployeeFIO { get; set; }

        [Display(Name = "Вес")]
        public double Weight { get; set; }
    }
}
