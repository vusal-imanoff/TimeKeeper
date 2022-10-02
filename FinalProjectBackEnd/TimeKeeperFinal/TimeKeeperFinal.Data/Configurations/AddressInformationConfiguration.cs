using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class AddressInformationConfiguration : IEntityTypeConfiguration<AddressInformation>
    {
        public void Configure(EntityTypeBuilder<AddressInformation> builder)
        {
            builder.Property(a => a.AppUserId).IsRequired(true);
            builder.Property(a => a.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.SurName).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.Email).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.Phone).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.Address).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.Country).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.City).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.ZipCode).IsRequired(true).HasMaxLength(255);
        }
    }
}
