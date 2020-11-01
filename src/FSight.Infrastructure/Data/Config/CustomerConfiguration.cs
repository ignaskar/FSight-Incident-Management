using FSight.Core.Entities;
using FSight.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSight.Infrastructure.Data.Config
{
    public class CustomerConfiguration : BaseUserConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Type).HasDefaultValue(UserType.Customer);
            builder.ToTable("Customers");
        }
    }
}