using FSight.Core.Entities;
using FSight.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSight.Infrastructure.Data.Config
{
    public class ProjectManagerConfiguration : BaseUserConfiguration<ProjectManager>
    {
        public override void Configure(EntityTypeBuilder<ProjectManager> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Type).HasDefaultValue(UserType.ProjectManager);
            builder.ToTable("ProjectManagers");
        }
    }
}