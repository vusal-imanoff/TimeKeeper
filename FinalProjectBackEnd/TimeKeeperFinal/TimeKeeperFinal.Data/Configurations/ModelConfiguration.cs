using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.Property(b => b.Name).IsRequired(true).HasMaxLength(255);
        }
    }
}
