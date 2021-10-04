using Calendar.AplicationService.Queries.EventItemAggregate;
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
    public class GetEventItemsQueryHandlerTests
    {
        private readonly IEventItemRepository _eventItemRepository;
        private readonly GetEventItemsQueryHandler _testee;
        private readonly List<EventItem> _eventItems;

        public GetEventItemsQueryHandlerTests()
        {
            _eventItemRepository = A.Fake<IEventItemRepository>();
            _testee = new GetEventItemsQueryHandler(_eventItemRepository);

            _eventItems = new List<EventItem> {
                new EventItem
                {
                    Id=Guid.NewGuid()
                },
                new EventItem
                {
                    Id=Guid.NewGuid()
                }
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnEventItems()
        {
            A.CallTo(() => _eventItemRepository.GetAll().ToList()).Returns(_eventItems);

            var result = await _testee.Handle(new GetEventItemsQuery(), default);

            A.CallTo(() => _eventItemRepository.GetAll()).MustHaveHappenedOnceExactly();
            result.Should().BeOfType<List<EventItemDto>>();
            result.Count.Should().Be(2);
        }
    }
}
