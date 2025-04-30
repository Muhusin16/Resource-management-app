using Microsoft.EntityFrameworkCore;
using ResourceManagementApp.Models.Entities;

namespace ResourceManagementApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Intern> Interns { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<TaskAssignment> Tasks { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }

}
