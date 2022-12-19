using Microsoft.EntityFrameworkCore;
using Npgsql;
using ProfileClasses;

namespace ClassLibrary
{
    public class ProjectDatabaseContext : DbContext
    {
        private readonly string _connectionString = "Server=localhost;Port=49153;Database=pg;Uid=postgres;Pwd=postgrespw;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
        public ProjectDatabaseContext(DbContextOptions<ProjectDatabaseContext> context) : base()
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }


        public async Task<bool> TestConnection()
        {
            await using (NpgsqlConnection conn = new NpgsqlConnection(_connectionString))
            {
                try
                {
                    await conn.OpenAsync();
                    var connected = (conn.State == System.Data.ConnectionState.Open);
                    await conn.CloseAsync();
                    return connected;
                }
                catch
                {
                    return false;
                }
            }
        }

    }
}
