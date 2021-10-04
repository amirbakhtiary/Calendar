using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Calendar.AplicationService.Test.Command
{
    public class AddEventItemCommandHandlerTests
    {
        private readonly AddNewEventItemCommandHandler _testee;
        private readonly IEventItemRepository _eventItemRepository;
        private readonly IUnitOfWork _UnitOfWork;

        public AddEventItemCommandHandlerTests()
        {
            _eventItemRepository = A.Fake<IEventItemRepository>();
            _testee = new AddNewEventItemCommandHandler(_eventItemRepository, _UnitOfWork);
        }

        [Fact]
        public async void Handle_ShouldCallAddAsync()
        {
            await _testee.Handle(new AddNewEventItemCommand(), default);

            A.CallTo(() => _eventItemRepository.AddAsync(A<EventItem>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedEventItem()
        {
            A.CallTo(() => _eventItemRepository.AddAsync(A<EventItem>._)).Returns(new EventItem
            {
                //Name = "Yoda"
            });

            var result = await _testee.Handle(new AddNewEventItemCommand(), default);

            result.Should().BeOfType<EventItem>();
            result.Name.Should().Be("Yoda");
        }
    }
}
