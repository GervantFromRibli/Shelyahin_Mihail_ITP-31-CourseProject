using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CourseProject.Models
{
    public class EmployeeIndexViewModel
    {
        public List<Employee> Employees { get; set; }

        public PageViewModel PageViewModel { get; set; }

        public List<int> Ids { get; set; }

        public List<string> FilterPositions { get; set; }

        public string FilterPosition { get; set; }

        public string FilterEducation { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "ФИО")]
        public string FIO { get; set; }

        [Display(Name = "Должность")]
        public string Position { get; set; }

        [Display(Name = "Образование")]
        public string Education { get; set; }
    }
}
