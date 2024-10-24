using Microsoft.EntityFrameworkCore;

namespace EFSQLiteSample
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "products.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }
    }
}
