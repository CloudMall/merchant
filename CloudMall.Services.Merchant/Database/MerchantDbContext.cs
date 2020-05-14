using Microsoft.EntityFrameworkCore;

namespace CloudMall.Services.Merchant.Database
{
    public class MerchantDbContext : DbContext
    {
        public MerchantDbContext(DbContextOptions<MerchantDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Merchant> Merchants { get; set; }
    }
}