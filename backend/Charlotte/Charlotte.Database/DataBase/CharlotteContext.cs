using Microsoft.EntityFrameworkCore;
using Charlotte.DataBase.Model;
using Charlotte.Services;
using Charlotte.Database.Model;

namespace Charlotte.DataBase.DbContextModel
{
    public class CharlotteContext : DbContext
    {
        public DbSet<UserMain> UserMain { get; set; }
        public DbSet<LoginLog> LoginLog { get; set; }
        public DbSet<RefreshTokenLog> RefreshTokenLog { get; set; }
        public DbSet<Router> Router { get; set; }
        public DbSet<ManagerRoleAuth> ManagerRoleAuth { get; set; }
        public DbSet<ProductType> ProductType { get; set; }
        public DbSet<ProductInformation> ProductInformation { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Factory> Factory { get; set; }
        public DbSet<ManagerMain> ManagerMain { get; set; }
        public DbSet<ManagerRole> ManagerRole { get; set; }
        public DbSet<ManagerLoginLog> ManagerLoginLog { get; set; }
        public DbSet<ManagerRefreshTokenLog> ManagerRefreshTokenLog { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder) 
        {
            var connectionString = GetAppSettingsUtils.GetConnectionString("Charlotte");
            optionBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetail>()
                .HasKey(a => new { a.OrderId, a.ProductId });
            modelBuilder.Entity<RefreshTokenLog>()
                .HasKey(a => new { a.RefreshToken, a.UserId });
            modelBuilder.Entity<ManagerRoleAuth>()
                .HasKey(a => new { a.RoleId, a.RouterId });
            modelBuilder.Entity<ManagerRefreshTokenLog>()
                .HasKey(a => new { a.RefreshToken, a.ManagerUserId });
        }
    }
}
