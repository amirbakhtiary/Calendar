using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemById;
using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using FakeItEasy;
using FluentAssertions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Calendar.AplicationService.Test.Query
{
    public class GetEventItemByIdQueryHandlerTests
    {
        private readonly IEventItemRepository _eventItemRepository;
        private readonly GetEventItemByIdQueryHandler _testee;

        public EventItem EventItem { get; }

        private readonly EventItem _eventItem;
        private readonly Guid _id = Guid.Parse("803a95ef-89c5-43d5-aa2c-82a3695d9894");

        public GetEventItemByIdQueryHandlerTests()
        {
            _eventItemRepository = A.Fake<IEventItemRepository>();
            _testee = new GetEventItemByIdQueryHandler(_eventItemRepository);

            EventItem = EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
            {
                Id = _id,
                Name = "Event"
            });
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldReturnEventItem()
        {
            A.CallTo(() => _eventItemRepository.GetEventItemByIdAsync(_id, default)).Returns(_eventItem);

            var result = await _testee.Handle(new GetEventItemByIdQuery(_id), default);

            A.CallTo(() => _eventItemRepository.GetEventItemByIdAsync(_id, default)).MustHaveHappenedOnceExactly();
            result.Name.Should().Be("Event");
        }
    }
}
