using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.Property(a => a.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(a => a.SurName).IsRequired(true).HasMaxLength(255);
        }
    }
}
