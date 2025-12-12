using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.Price).HasPrecision(18, 2).IsRequired();
            builder.Property(p => p.StockQuantity).IsRequired().HasDefaultValue(0);
            builder.Property(p => p.Sku).IsRequired().HasMaxLength(50);
            builder.HasIndex(p => p.Sku).IsUnique();
            builder.Property(p => p.CreatedAt).IsRequired().HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(p => p.UpdatedAt).IsRequired(false);
            builder.Property(p => p.RowVersion).IsRowVersion();
        }
    }
}
