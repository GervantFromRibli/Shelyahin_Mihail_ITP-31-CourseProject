using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    // Модель, представляющая запись в таблице Работники
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Position { get; set; }
        public string Education { get; set; }
        public Employee() { }
    }
}
