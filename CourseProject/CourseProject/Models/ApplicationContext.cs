using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    // Класс контекста данных
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Furniture> Furniture { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Waybill> Waybills { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
    }
}
