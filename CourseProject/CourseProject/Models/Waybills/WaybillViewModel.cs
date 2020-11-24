using System;

namespace CourseProject.Models
{
    // Модель отображения записей из таблицы Накладные с заменой внешних ключей смысловыми данными
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
