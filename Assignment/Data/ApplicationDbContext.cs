using Assignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }

        // DbSets for both Component and RM
        public DbSet<RmComponent> Components { get; set; }
        public DbSet<RM> RMs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Any specific model configurations can go here

            // Define the relationship (One Component has many RMs)
            modelBuilder.Entity<RmComponent>()
                .HasMany(c => c.RMData)
                .WithOne(r => r.Component)
                .HasForeignKey(r => r.ComponentId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
