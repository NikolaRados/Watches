using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Watches.Domain;

namespace Watches.DataAccess.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.Property(x => x.Description).IsRequired();

            builder.HasMany(p => p.OrderLines)
                .WithOne(ol => ol.Product)
                .HasForeignKey(ol => ol.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
