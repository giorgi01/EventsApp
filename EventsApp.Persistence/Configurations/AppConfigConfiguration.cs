using EventsApp.Domain;
using EventsApp.Domain.POCO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsApp.Persistence.Configurations
{
    class AppConfigEntityConfiguration : IEntityTypeConfiguration<AppConfig>
    {
        public void Configure(EntityTypeBuilder<AppConfig> builder)
        {
            builder.HasKey(x => x.Setting);
            builder.HasIndex(x => new { x.Setting }).IsUnique();
            builder.Property(x => x.Setting).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Value).IsRequired();
            builder.Property(x => x.Description).IsRequired();
        }
    }
}
