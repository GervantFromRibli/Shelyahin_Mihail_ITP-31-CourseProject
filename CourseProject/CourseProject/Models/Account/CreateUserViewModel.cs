using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseProject.Models
{
    public class CreateUserViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string FIO { get; set; }

        public int Age { get; set; }
    }
}
