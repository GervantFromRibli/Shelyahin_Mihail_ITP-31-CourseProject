using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    // Модель, представляющая запись в таблице Мебель
    public class Furniture
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public Furniture() { }
    }
}
