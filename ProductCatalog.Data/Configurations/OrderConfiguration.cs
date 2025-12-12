using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Enums;

namespace ProductCatalog.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);
            builder.Property(o => o.OrderDate).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
            builder.Property(o => o.TotalAmount).HasPrecision(18, 2).IsRequired();
            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(20).HasDefaultValue(OrderStatus.Pending);
            builder.Property(o => o.CustomerEmail).IsRequired().HasMaxLength(200);
            builder.Property(o => o.CustomerName).IsRequired().HasMaxLength(200);
            builder.HasIndex(o => o.OrderNumber).IsUnique();
        }
    }
}
