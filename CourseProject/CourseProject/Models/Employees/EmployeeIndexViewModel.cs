using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class EmployeeIndexViewModel
    {
        public List<Employee> Employees { get; set; }

        public List<int> Ids { get; set; }

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
