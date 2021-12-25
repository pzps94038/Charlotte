using Microsoft.EntityFrameworkCore;
using Charlotte.Model;


namespace Charlotte.DbContextModel
{
    public class CharlotteContext : DbContext
    {
        DbSet<UserMain> UserMain { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) 
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("Charlotte");
            optionBuilder.UseSqlServer(connectionString);
        }
    }
}
