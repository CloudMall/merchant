using CloudMall.Services.Merchant.Models;
using Microsoft.EntityFrameworkCore;
using WeihanLi.EntityFramework.Audit;

namespace CloudMall.Services.Merchant.Database
{
    public class MerchantDbContext : AuditDbContext
    {
        public MerchantDbContext(DbContextOptions<MerchantDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Merchant> Merchants { get; set; }

        public DbSet<MerchantCategory> MerchantCategories { get; set; }

        public DbSet<MerchantManager> MerchantManagers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Merchant>()
                .HasQueryFilter(m => m.IsDeleted == false);
        }
    }
}