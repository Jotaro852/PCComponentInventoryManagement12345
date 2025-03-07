using Microsoft.EntityFrameworkCore;
using PCComponentInventoryManagement.Models;

namespace PCComponentInventoryManagement.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Component> Components { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<PCConfiguration> PCConfigurations { get; set; }
        public DbSet<ConfigurationComponent> ConfigurationComponents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=1234;Include Error Detail=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConfigurationComponent>()
                .HasOne(cc => cc.Configuration)
                .WithMany(pc => pc.Components)
                .HasForeignKey(cc => cc.PCConfigurationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConfigurationComponent>()
                .HasOne(cc => cc.Component)
                .WithMany()
                .HasForeignKey(cc => cc.ComponentId);
        }
    }
}