using Microsoft.EntityFrameworkCore;

namespace Lab6.Models
{
    // Класс контекста данных
    public class ApplicationContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Waybill> Waybills { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
