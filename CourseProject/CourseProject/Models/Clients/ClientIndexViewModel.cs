using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class ClientIndexViewModel
    {
        public List<Client> Clients { get; set; }

        public List<int> Ids { get; set; }

        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "ФИО представителя")]
        public string RepresentativeFIO { get; set; }

        [Display(Name = "Номер")]
        public int Number { get; set; }

        [Display(Name = "Адрес")]
        public string Address { get; set; }
    }
}
