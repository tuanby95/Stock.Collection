using Microsoft.EntityFrameworkCore;
using Stock.Collection.DataAccess.Entities;

namespace Stock.Collection.DataAccess.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
        }
    }
}
