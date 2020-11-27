using Microsoft.AspNetCore.Identity;

namespace CourseProject.Models
{
    public class User : IdentityUser
    {
        public string FIO { get; set; }

        public int Age { get; set; }
    }
}
