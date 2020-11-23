using FSight.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSight.Infrastructure.Data.Config
{
    public class ProjectsConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ProjectManagerId).IsRequired();
            builder.Property(x => x.CreateDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.LastUpdated).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}