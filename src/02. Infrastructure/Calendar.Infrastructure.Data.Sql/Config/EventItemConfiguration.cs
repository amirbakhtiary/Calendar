using Calendar.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendar.Infrastructure.Data.Sql.Config
{
    public class EventItemConfiguration : IEntityTypeConfiguration<EventItem>
    {
        public void Configure(EntityTypeBuilder<EventItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasDefaultValueSql("(newid())");
            builder.Property(e => e.EventTime)
                .IsRequired()
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.Property(e => e.Name).HasMaxLength(250);

            builder.HasOne(e => e.EventOrganizer).WithMany(m => m.EventOrganizers);
            builder.HasMany(e => e.Members).WithMany(m => m.EventItems);
        }
    }
}
