using Calendar.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Calendar.Infrastructure.Data.Sql
{
    public class CalendarDBContext: DbContext
    {
        public DbSet<EventItem> EventItems { get; set; }

        public CalendarDBContext(DbContextOptions<CalendarDBContext> options) : base(options)
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
