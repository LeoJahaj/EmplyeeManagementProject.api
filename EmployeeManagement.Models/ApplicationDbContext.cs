using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TaskEntity> Tasks { get; set; }  // This defines the Tasks table in the database
        public DbSet<ProjectEntity> Projects { get; set; }  // This defines the Projects table in the database
    }
}
