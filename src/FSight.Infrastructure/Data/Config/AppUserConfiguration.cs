using FSight.Core.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSight.Infrastructure.Data.Config
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.EmployeeNumber).HasMaxLength(10);
            builder.Property(x => x.UserName).HasMaxLength(50);
            builder.Property(x => x.CreateDate).HasDefaultValueSql("GETUTCDATE()");
            builder.Property(x => x.LastUpdated).HasDefaultValueSql("GETUTCDATE()");
        }
    }
}