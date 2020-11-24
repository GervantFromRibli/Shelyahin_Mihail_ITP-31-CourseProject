namespace CourseProject.Models
{
    // Модель отображения записей из таблицы Заказы с заменой внешних ключей смысловыми данными
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
