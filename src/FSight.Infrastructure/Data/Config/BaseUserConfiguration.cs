using System;
using FSight.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSight.Infrastructure.Data.Config
{
    public class BaseUserConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity: BaseUser
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CreateDate).HasDefaultValue(DateTime.UtcNow);
            builder.Property(x => x.LastUpdated).HasDefaultValue(DateTime.UtcNow);
        }
    }
}