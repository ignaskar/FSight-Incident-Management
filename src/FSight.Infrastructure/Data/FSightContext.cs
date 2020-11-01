using FSight.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FSight.Infrastructure.Data
{
    public class FSightContext : DbContext
    {
        public FSightContext(DbContextOptions<FSightContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Developer>().ToTable("Developers");
            modelBuilder.Entity<Customer>().ToTable("Customers");
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}