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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Country>()
                .HasIndex(country => country.Name)
                .IsUnique();
        }
    }
}
