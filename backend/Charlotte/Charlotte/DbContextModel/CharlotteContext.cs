using Microsoft.EntityFrameworkCore;
using Charlotte.Model;
using Charlotte.Helper;
using Charlotte.Model.DataBase;

namespace Charlotte.DbContextModel
{
    public class CharlotteContext : DbContext
    {
        public DbSet<UserMain> UserMain { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<RefreshTokenLog> RefreshTokenLog { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<LoginLog> LoginLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) 
        {
            var connectionString = GetAppSettingsHelper.GetConnectionString("Charlotte");
            optionBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(a => new { a.ProductId, a.UserId });
            modelBuilder.Entity<RefreshTokenLog>()
                .HasKey(a => new { a.RefreshToken, a.UserId });
        }
    }
}
