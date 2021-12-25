using Microsoft.EntityFrameworkCore;
using Charlotte.Model;


namespace Charlotte.DbContextModel
{
    public class CharlotteContext : DbContext
    {
        DbSet<UserMain> UserMain { get; set; }
        DbSet<ProductType> ProductType { get; set; }
        DbSet<ProductDetail> ProductDetails { get; set; }
        DbSet<RefreshTokenStatus> RefreshTokenStatus { get; set; }
        DbSet<OrderDetail> OrderDetail { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) 
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("Charlotte");
            optionBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(a => new { a.ProductId, a.UserId });
            modelBuilder.Entity<RefreshTokenStatus>()
                .HasKey(a => new { a.RefreshToken, a.UserId });
        }
    }
}
