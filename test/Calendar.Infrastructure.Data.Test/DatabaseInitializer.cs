using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Infrastructure.Data.Test
{
    public class DatabaseInitializer
    {
        public static void Initialize(CalendarContext context)
        {
            if (context.EventItems.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(CalendarContext context)
        {
            var eventItems = GetFake_EventItems();

            context.EventItems.AddRange(eventItems);
            context.SaveChanges();
        }

        private static IEnumerable<EventItem> GetFake_EventItems()
        {
            return new List<EventItem> {
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    Name = "Interview",
                    EventTime = DateTime.Now.AddDays(7),
                    Location = "New York",
                    Members = new List<string> { "Smith", "Amir" },
                    EventOrganizer = "John"
                }),
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    Name = "Metting",
                    EventTime = DateTime.Now.AddDays(1),
                    Location = "Texas",
                    Members = new List<string> { "Barry", "Amir" },
                    EventOrganizer = "Alex"
                }),
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Id = Guid.Parse("971316e1-4966-4426-b1ea-a36c9dde1066"),
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
