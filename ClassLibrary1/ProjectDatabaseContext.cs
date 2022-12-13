using Microsoft.EntityFrameworkCore;
using ProfileClasses;

namespace ClassLibrary
{
    public class ProjectDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Database=postgres;Port=5432;User Id=postgres;Password=postgrepw");
        }
        public ProjectDatabaseContext(DbContextOptions<ProjectDatabaseContext> context) : base()
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }

    }
}
