using FruitApi.Models;
using Microsoft.EntityFrameworkCore;



namespace FruitApi.Database
{
    public class FruitDbContext:DbContext
    {
        public FruitDbContext(DbContextOptions<FruitDbContext> options) : base(options)
        {
        }

        public DbSet<Fruit> Fruits { get; set; }
        public DbSet<Nutritions> Nutritions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Fruit>()
                .HasOne(f => f.nutritions)
                .WithOne(n => n.Fruit)
                .HasForeignKey<Nutritions>(n => n.FruitId);

            modelBuilder.Entity<Nutritions>()
                .HasKey(n => n.Id);
        }
    }
}
