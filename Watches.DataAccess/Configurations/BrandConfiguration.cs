using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Domain;

namespace Watches.DataAccess.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(g => g.Products)
                   .WithOne(u => u.Brand)
                   .HasForeignKey(u => u.BrandId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
