using EmployeeManagementSystem.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Data
{
    public class EmployeeDataDbContext : DbContext
    {
        public EmployeeDataDbContext(DbContextOptions<EmployeeDataDbContext> options):base(options) { 
        }

        public DbSet<Employee> employees { get; set; }
    }
}
