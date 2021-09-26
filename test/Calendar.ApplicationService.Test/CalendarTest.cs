using AutoMapper;
using Calendar.Core.Domain;
using Calendar.Endpoints.WebAPI.Controllers;
using Calendar.Infrastructure.Data.Sql;
using Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Calendar.Endpoints.WebAPI.Test
{
    public class CalendarTest
    {
        [Fact]
        public void Check_GetAll_Success()
        {
            using (var context = new CalendarDBContext(contextOption))
            {
                context.AddRange(GetFake_EventItems());
                context.SaveChanges();
            }

            using (var context = new CalendarDBContext(contextOption))
            {
                var repository = new CalendarRepository(context);

                Mock<IMapper> mockMapper = new Mock<IMapper>();
                Mock<IMediator> mockMediator = new Mock<IMediator>();
                Mock<ILogger<CalendarController>> mockLogger = new Mock<ILogger<CalendarController>>();

                IMapper mapper = mockMapper.Object;
                IMediator mediator = mockMediator.Object;
                ILogger<CalendarController> logger = mockLogger.Object;

                var controller = new CalendarController(logger, mapper, mediator);
                var actionResult = controller.GetEventItems().Result;
                var viewResult = Assert.IsType<OkObjectResult>(actionResult);
                var model = Assert.IsAssignableFrom<IEnumerable<GetEventItemsDto>>(viewResult.Value);

                Assert.Equal(3, model.Count());
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
