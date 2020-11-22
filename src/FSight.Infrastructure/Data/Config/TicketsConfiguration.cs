using FSight.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSight.Infrastructure.Data.Config
{
    public class TicketsConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.Property(x => x.Number).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(75);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.State).IsRequired();
            builder.Property(x => x.Priority).IsRequired();
            builder.Property(x => x.Category).IsRequired();
        }
    }
}