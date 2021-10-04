using Calendar.AplicationService.Queries.EventItemAggregate;
using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemByOrganizer;
using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItems;
using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Calendar.AplicationService.Test.Query
{
    public class GetEventItemByOrganizerQueryHandlerTests
    {
        private readonly IEventItemRepository _eventItemRepository;
        private readonly GetEventItemByOrganizerQueryHandler _testee;
        private readonly List<EventItem> _eventItems;
        private readonly string _eventOrganizer = "John";

        public GetEventItemByOrganizerQueryHandlerTests()
        {
            _eventItemRepository = A.Fake<IEventItemRepository>();
            _testee = new GetEventItemByOrganizerQueryHandler(_eventItemRepository);

            _eventItems = new List<EventItem>
            {
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Id = new Guid(),
                    Name = "Interview",
                    EventOrganizer = _eventOrganizer
                }),
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Id = new Guid(),
                    Name = "Metting",
                    EventOrganizer = _eventOrganizer
                }),
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnEventItemsByOrganizer()
        {
            A.CallTo(() => _eventItemRepository.GetAll().Where(e => e.EventOrganizer.Name == _eventOrganizer).ToList()).Returns(_eventItems);

            var result = await _testee.Handle(new GetEventItemByOrganizerQuery(_eventOrganizer), default);

            A.CallTo(() => _eventItemRepository.GetAll()).MustHaveHappenedOnceExactly();
            result.Should().BeOfType<List<EventItemDto>>();
            result.Count.Should().Be(2);
        }
    }
}
