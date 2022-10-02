using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class SizeConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(s => s.Name).IsRequired(true).HasMaxLength(255);
        }
    }
}
