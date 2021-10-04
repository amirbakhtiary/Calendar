using Calendar.AplicationService.Commands.EventItemAggregate.RemoveEventItem;
using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Calendar.AplicationService.Test.Command
{
    public class RemoveEventItemCommandHandlerTests
    {
        private readonly RemoveEventItemCommandHandler _testee;
        private readonly IEventItemRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EventItem _eventItem;

        public RemoveEventItemCommandHandlerTests()
        {
            _eventItemRepository = A.Fake<IEventItemRepository>();
            _unitOfWork = A.Fake<IUnitOfWork>();
            _testee = new RemoveEventItemCommandHandler(_eventItemRepository, _unitOfWork);

            _eventItem = new EventItem
            {
                //Name = "Yoda"
            };
        }

        [Fact]
        public async void Handle_ShouldReturnRemoveTrue()
        {
            A.CallTo(() => _eventItemRepository.Remove(A<EventItem>._));

            var result = await _testee.Handle(new RemoveEventItemCommand(), default);

            result.Should().BeTrue();
            result.Should().Be(true);
        }

        [Fact]
        public async void Handle_ShouldRemove()
        {
            await _testee.Handle(new RemoveEventItemCommand(), default);

            A.CallTo(() => _eventItemRepository.Remove(A<EventItem>._)).MustHaveHappenedOnceExactly();
        }
    }
}
