using Calendar.Infrastructure.Data.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Infrastructure.Data.Test
{
    public class DatabaseTestBase : IDisposable
    {
        protected readonly CalendarContext Context;

        public DatabaseTestBase()
        {
            var options = new DbContextOptionsBuilder<CalendarContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            Context = new CalendarContext(options);

            Context.Database.EnsureCreated();

            DatabaseInitializer.Initialize(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();

            Context.Dispose();
        }
    }
}
