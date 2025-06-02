using EmployeeManagment.Models;
using EmployeeManagment.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagment.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions options) : base(options) 
       
        {
           
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
