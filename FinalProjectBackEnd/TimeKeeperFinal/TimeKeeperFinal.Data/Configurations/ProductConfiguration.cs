using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(p => p.Description).IsRequired(true).HasMaxLength(1000);
            builder.Property(p => p.Price).IsRequired(true).HasColumnType("money");
            builder.Property(p => p.DiscountPrice).IsRequired(true).HasColumnType("money");
            builder.Property(p => p.MainImage).HasMaxLength(1000);
            builder.Property(p => p.SecondImage).HasMaxLength(1000);
            builder.Property(p => p.Code).IsRequired(true).HasMaxLength(20);
            builder.Property(p => p.Count).IsRequired(true);
        }
    }
}
