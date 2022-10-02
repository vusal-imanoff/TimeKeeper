using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(b => b.MainTitle).IsRequired(true).HasMaxLength(255);
            builder.Property(b => b.SubTitle).IsRequired(true).HasMaxLength(255);
            builder.Property(b => b.Description).IsRequired(true).HasMaxLength(255);
            builder.Property(b => b.Image).HasMaxLength(1000);
        }
    }
}
