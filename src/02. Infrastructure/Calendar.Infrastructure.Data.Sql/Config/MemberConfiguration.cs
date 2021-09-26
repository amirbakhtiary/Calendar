using Calendar.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Calendar.Infrastructure.Data.Sql.Config
{
    class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name).HasMaxLength(120);
            //builder.HasMany(m => m.Events).WithMany(e => e.Members);
            //builder.HasMany(m => m.EventOrganizers).WithOne(e => e.EventOrganizer);
        }
    }
}
