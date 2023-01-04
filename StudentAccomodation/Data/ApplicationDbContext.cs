using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentAccomodation.Models;

namespace StudentAccomodation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<House> Houses { get; set; }
        public DbSet<Student> Students { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}