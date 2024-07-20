using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<Product>().Property(x => x.Name).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Product>().Property(x => x.Description).HasMaxLength(128).IsRequired();
        }
    }
}
