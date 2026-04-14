using Microsoft.EntityFrameworkCore;
using FinalProject.Models;
using Microsoft.Identity.Client;


namespace FinalProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<TeamMember>TeamMembers { get; set; }
        public DbSet<Hobby>Hobbies { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Movie>Movies { get; set; }

    }
}
