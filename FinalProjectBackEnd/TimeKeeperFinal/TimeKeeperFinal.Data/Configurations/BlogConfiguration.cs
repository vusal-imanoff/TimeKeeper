using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeperFinal.Core.Entities;

namespace TimeKeeperFinal.Data.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(b => b.Name).IsRequired(true).HasMaxLength(255);
            builder.Property(b => b.MainBlog).IsRequired(true).HasMaxLength(1000);
            builder.Property(b => b.SubBlog).IsRequired(true).HasMaxLength(1000);
            builder.Property(b => b.Image).HasMaxLength(1000);
        }
    }
}
