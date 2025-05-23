using CoreAPI.EFLodingQuery;
using Microsoft.EntityFrameworkCore;

namespace CoreAPI
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies()
                .UseSqlServer("Server=YOUR_SERVER_NAME;Database=EFCoreLoadingDemoDB;Trusted_Connection=True;");
        }
    }
}
