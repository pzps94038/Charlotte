using Microsoft.EntityFrameworkCore;
using Charlotte.Model;
using Charlotte.Helper;

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
            var connectionString = GetAppSettingsHelper.GetConnectionString("Charlotte");
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
