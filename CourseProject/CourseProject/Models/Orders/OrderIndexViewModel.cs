using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class OrderIndexViewModel
    {
        public List<OrderViewModel> OrderViewModels { get; set; }

        public List<string> EmployeesFIOs { get; set; }

        public List<string> ClientNames { get; set; }

        public List<string> FurnitureNames { get; set; }

        public List<int> Ids { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название клиента")]
        public string ClientName { get; set; }

        [Display(Name = "Название мебели")]
        public string FurnitureName { get; set; }

        [Display(Name = "Количество мебели")]
        public int FurnitureCount { get; set; }

        [Display(Name = "Стоимость")]
        public decimal Price { get; set; }

        [Display(Name = "Процент скидки")]
        public int DiscountPercent { get; set; }

        [Display(Name = "Выполнено?")]
        public bool IsCompleted { get; set; }

        [Display(Name = "ФИО работника")]
        public string EmployeeFIO { get; set; }
    }
}
