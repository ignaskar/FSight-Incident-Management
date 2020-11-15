using FSight.Core.Entities;
using FSight.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSight.Infrastructure.Data.Config
{
    public class DeveloperConfiguration : BaseUserConfiguration<Developer>
    {
        public override void Configure(EntityTypeBuilder<Developer> builder)
        {
            base.Configure(builder);
            builder.ToTable("Developers");
        }
    }
}