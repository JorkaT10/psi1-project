using Microsoft.EntityFrameworkCore;
using ProfileClasses;

namespace ClassLibrary
{
    public class ProjectDatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=49153;Database=MyTestServer;Uid=postgres;Pwd=postgrespw;");
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Ratings> Ratings { get; set; }

    }
}
