
using Microsoft.EntityFrameworkCore;
using PS_EMS.Models;

namespace PS_EMS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
    }
}