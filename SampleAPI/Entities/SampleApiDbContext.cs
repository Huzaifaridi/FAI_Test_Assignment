using Microsoft.EntityFrameworkCore;

namespace SampleAPI.Entities
{
    public class SampleApiDbContext : DbContext
    {
        public SampleApiDbContext() { }
        public SampleApiDbContext(DbContextOptions<SampleApiDbContext> options) :
            base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p =>p.Name).HasMaxLength(10).IsRequired();
                entity.Property(p => p.Desscription).HasMaxLength(10).IsRequired();
                entity.Property(p => p.Id).ValueGeneratedOnAdd();
                entity.Property(p => p.InvoiceGenerated).ValueGeneratedOnAdd();
                entity.Property(p => p.OrderDeleted).ValueGeneratedOnAdd();

            }); 
        }
    }
}
