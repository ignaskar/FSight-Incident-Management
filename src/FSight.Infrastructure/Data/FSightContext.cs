using System;
using System.Reflection;
using FSight.Core.Entities;
using FSight.Core.Entities.Identity;
using FSight.Infrastructure.Data.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FSight.Infrastructure.Data
{
    public class FSightContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public FSightContext(DbContextOptions<FSightContext> options) : base(options)
        {
            
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        // public DbSet<ProjectManager> ProjectManagers { get; set; }
        // public DbSet<Developer> Developers { get; set; }
        // public DbSet<Customer> Customers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}