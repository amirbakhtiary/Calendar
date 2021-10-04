using Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem;
using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Calendar.AplicationService.Test.Command
{
    public class UpdateEventItemCommandHandlerTests
    {
        private readonly UpdateEventItemCommandHandler _testee;
        private readonly IEventItemRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventItem _eventItem;

        public UpdateEventItemCommandHandlerTests()
        {
            _eventItemRepository = A.Fake<IEventItemRepository>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _testee = new UpdateEventItemCommandHandler(_eventItemRepository, _unitOfWork);

            _eventItem = EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
            {
                Name = "Event"
            });
        }

        [Fact]
        public async void Handle_ShouldReturnUpdatedEventItem()
        {
            A.CallTo(() => _eventItemRepository.Update(A<EventItem>._)).Returns(_eventItem);

            var result = await _testee.Handle(new UpdateEventItemCommand(), default);

            result.Should().BeOfType<EventItem>();
            result.Data.Name.Should().Be(_eventItem.Name);
        }

        [Fact]
        public async void Handle_ShouldUpdateAsync()
        {
            await _testee.Handle(new UpdateEventItemCommand(), default);

            A.CallTo(() => _eventItemRepository.Update(A<EventItem>._)).MustHaveHappenedOnceExactly();
        }
    }
}
