using Calendar.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Calendar.Infrastructure.Data.Sql
{
    public class CalendarContext: DbContext
    {
        public DbSet<EventItem> EventItems { get; set; }

        public CalendarContext()
        {
        }
        public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
