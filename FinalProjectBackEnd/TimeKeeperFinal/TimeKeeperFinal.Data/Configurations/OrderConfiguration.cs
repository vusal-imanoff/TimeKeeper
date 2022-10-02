using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(o => o.ProductId).IsRequired(true);
            builder.Property(o => o.AddressInformationId).IsRequired(true);
            builder.Property(o => o.Count).IsRequired(true);
            builder.Property(o => o.TotalPrice).IsRequired(true).HasColumnType("money");


        }
    }
}
