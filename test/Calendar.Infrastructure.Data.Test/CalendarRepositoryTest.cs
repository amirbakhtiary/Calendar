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
        [Fact]
        public async void Check_GetById()
        {
            var _firstId = PrepareFakeData();

            using (var context = new CalendarDBContext(contextOption))
            {
                var repository = new CalendarRepository(context);
                var found = await repository.GetByIdAsync(_firstId);
                Assert.Equal(_firstId, found.Id);
            }
        }

        [Fact]
        public async void Check_GetAll()
        {
            PrepareFakeData();
            using (var context = new CalendarDBContext(contextOption))
            {
                var repository = new CalendarRepository(context);
                var count = await repository.TableNoTracking.CountAsync();
                Assert.Equal(3, count);
            }
        }

        [Fact]
        public async void Check_GetAllOrderByTime()
        {
            var _firstId = PrepareFakeData();
            using (var context = new CalendarDBContext(contextOption))
            {
                var repository = new CalendarRepository(context);
                var found = await repository.TableNoTracking.OrderByDescending(e => e.EventTime).FirstOrDefaultAsync();
                Assert.Equal(_firstId, found.Id);
            }
        }

        [Fact]
        public async void Check_GetAllByOrganizer()
        {
            var _firstId = PrepareFakeData();
            using (var context = new CalendarDBContext(contextOption))
            {
                var repository = new CalendarRepository(context);
                var found = await context.EventItems.OrderByDescending(e=>e.EventTime).FirstOrDefaultAsync(e => e.EventOrganizer.Name == "John");
                Assert.Equal(_firstId, found.Id);
            }
        }

        [Fact]
        public async Task Check_Add_RemoveItem()
        {
            PrepareFakeData();
            var eventItem = EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
            {
                Name = "Add Event",
                EventTime = DateTime.Now.AddDays(5),
                Location = "Paris",
                Members = new List<string> { "Smith", "Amir" },
                EventOrganizer = "Taher"
            });
            using (var context = new CalendarDBContext(contextOption))
            {
                var repository = new CalendarRepository(context);
                await repository.AddItemAsync(eventItem);
                await context.SaveChangesAsync();
                var count = await context.EventItems.CountAsync(e => e.Id == eventItem.Id);
                Assert.Equal(1, count);
            }

            using (var context = new CalendarDBContext(contextOption))
            {
                var repository = new CalendarRepository(context);
                repository.DeleteItem(eventItem);
                await context.SaveChangesAsync();

                var count = await context.EventItems.CountAsync(e => e.Id == eventItem.Id);
                Assert.Equal(0, count);
            }
        }
        private DbContextOptions<CalendarDBContext> contextOption
        {
            get
            {
                return new DbContextOptionsBuilder<CalendarDBContext>()
                    .UseInMemoryDatabase(databaseName: "CalendarDB")
                    .Options;
            }
        }
        private Guid PrepareFakeData()
        {
            var fakeData = GetFake_EventItems();
            using (var context = new CalendarDBContext(contextOption))
            {
                context.AddRange(fakeData);
                context.SaveChanges();
            }

            return fakeData.OrderByDescending(e => e.EventTime).FirstOrDefault().Id;
        }
        private IEnumerable<EventItem> GetFake_EventItems()
        {
            return new List<EventItem> {
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Name = "Interview",
                    EventTime = DateTime.Now.AddDays(7),
                    Location = "New York",
                    Members = new List<string> { "Smith", "Amir" },
                    EventOrganizer = "John"
                }),
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Name = "Metting",
                    EventTime = DateTime.Now.AddDays(1),
                    Location = "Texas",
                    Members = new List<string> { "Barry", "Amir" },
                    EventOrganizer = "Alex"
                }),
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Name = "ASP.NET Core Course",
                    EventTime = DateTime.Now.AddDays(3),
                    Location = "Tehran",
                    Members = new List<string> { "Rasoul", "Amir" ,"Vahid"},
                    EventOrganizer = "Nik"
                })
            };
        }
    }
}
