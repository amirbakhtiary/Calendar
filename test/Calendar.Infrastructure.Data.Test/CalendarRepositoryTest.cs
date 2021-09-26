using Calendar.Core.Domain;
using Calendar.Infrastructure.Data.Sql;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Calendar.Infrastructure.Data.Test
{
    public class CalendarRepositoryTest
    {
        private CalendarDBContext _context;
        private Guid _firstId;
        public CalendarRepositoryTest()
        {
            _context = new CalendarDBContext(ContextOptions);
            _firstId = PrepareData();
        }
        private DbContextOptions<CalendarDBContext> ContextOptions
        {
            get
            {
                var builder = new DbContextOptionsBuilder<CalendarDBContext>();
                builder.UseInMemoryDatabase(databaseName: "CalendarDB");
                return builder.Options;
            }
        }
        private Guid PrepareData()
        {
            var prepareData = GetAllData();
            _context.AddRange(prepareData);
            _context.SaveChanges();

            return prepareData.FirstOrDefault().Id;
        }
        [Fact]
        public async void Check_GetById()
        {
            var repository = new CalendarRepository(_context);
            var found = await repository.GetByIdAsync(_firstId);
            Assert.Equal(_firstId, found.Id);
        }

        [Fact]
        public async void Check_Manual_Query()
        {
            var repository = new CalendarRepository(_context);
            var count = await repository.TableNoTracking.CountAsync(e => e.EventOrganizer.Name == "John");
            Assert.Equal(1, count);
        }

        [Fact]
        public async Task Check_AddItem()
        {
            var repository = new CalendarRepository(_context);

            await repository.AddItemAsync(EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
            {
                Name = "Interview",
                EventTime = DateTime.Now,
                Location = "New York",
                Members = new List<string> { "Smith", "Amir" },
                EventOrganizer = "John"
            }));
            await _context.SaveChangesAsync();

            var count = await repository.TableNoTracking.CountAsync();
            Assert.Equal(3, count);
        }

        [Fact]
        public async Task Check_RemoveItem()
        {
            var repository = new CalendarRepository(_context);

            var eventItem = await repository.GetByIdAsync(_firstId);
            repository.DeleteItem(eventItem);
            await _context.SaveChangesAsync();

            var count = await _context.EventItems.CountAsync();
            Assert.Equal(1, count);
        }

        private List<EventItem> GetAllData()
        {
            return new List<EventItem> {
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Name = "Interview",
                    EventTime = DateTime.Now,
                    Location = "New York",
                    Members = new List<string> { "Smith", "Amir" },
                    EventOrganizer = "John"
                }),
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Name = "Metting",
                    EventTime = DateTime.Now,
                    Location = "Texas",
                    Members = new List<string> { "Barry", "Amir" },
                    EventOrganizer = "Alex"
                })
            };
        }
    }
}
