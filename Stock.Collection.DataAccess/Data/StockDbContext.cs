using Microsoft.EntityFrameworkCore;
using Stock.Collection.DataAccess.Entities;

namespace Stock.Collection.DataAccess.Data
{
    public class StockDbContext : DbContext
    {
        public StockDbContext()
        {

        }
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }
        public DbSet<Company> Companies { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=StockCollection1;Trusted_Connection=True;MultipleActiveResultSets=true");
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<Company>().HasKey("Id");
        }
    }
}