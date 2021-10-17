using EventsApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventsApp.Persistence.Configurations
{
    class EventEntityConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.Id }).IsUnique();

            builder.ToTable("Events");
            builder.Property(x => x.Title).IsRequired().HasMaxLength(40);
            builder.Property(x => x.Description).HasMaxLength(300);
            builder.Property(x => x.Address).HasMaxLength(40);
            builder.Property(x => x.PlannedAt).HasColumnType("datetime");
            builder.Property(x => x.CreatedAt).HasColumnType("datetime");
            builder.Property(x => x.IsActive);
            builder.Property(x => x.ImageUrl);
            builder.Property(x => x.UserId);
            
            builder.HasOne(e => e.User)
            .WithMany(u => u.Events)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
