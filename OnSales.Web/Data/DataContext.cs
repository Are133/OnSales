using Microsoft.EntityFrameworkCore;
using OnSales.Common.Entities;

namespace OnSales.Web.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Estate> Estates { get; set; }

        public DbSet<Municipality> Municipalities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                .HasIndex(country => country.Name)
                .IsUnique();

            modelBuilder.Entity<Estate>()
                .HasIndex(country => country.Name)
                .IsUnique();
        }
    }
}
