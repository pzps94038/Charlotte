using Microsoft.EntityFrameworkCore;
using Charlotte.DataBase.Model;
using Charlotte.Services;

namespace Charlotte.DataBase.DbContextModel
{
    public class CharlotteContext : DbContext
    {
        private string ConnectionString { get; set; }
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
