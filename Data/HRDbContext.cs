using HRManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HRManagementAPI.Data
{
    public class HRDbContext : DbContext
    {
        public HRDbContext(DbContextOptions<HRDbContext> options)
            : base(options)
        {
        }


        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }
    }
}