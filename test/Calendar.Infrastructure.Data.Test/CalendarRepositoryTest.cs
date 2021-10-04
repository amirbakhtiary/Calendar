using Calendar.Core.Domain;
using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql;
using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Calendar.Infrastructure.Data.Test
{
    public class CalendarRepositoryTest : DatabaseTestBase
    {
        private readonly CalendarContext _CalendarContext;
        private readonly Repository<EventItem> _testee;
        private readonly Repository<EventItem> _testeeFake;
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IUnitOfWork _UnitOfWorkFake;
        private readonly EventItem _newEventItem;

        public CalendarRepositoryTest()
        {
            _CalendarContext = A.Fake<CalendarContext>();
            _testeeFake = new Repository<EventItem>(_CalendarContext);
            _testee = new Repository<EventItem>(Context);
            _UnitOfWorkFake = new UnitOfWork(_CalendarContext);
            _UnitOfWork = new UnitOfWork(Context);

            _newEventItem = EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
            {
                Name = "New Event",
                EventOrganizer = "Son",
                EventTime = new DateTime(2020, 09, 16),
                Location = "Van",
                Members = new List<string> { "Johnson" }
            });
        }

        [Theory]
        [InlineData("Changed")]
        public void UpdateEventItem_WhenEventItemIsNotNull_ShouldReturnEventItem(string eventName)
        {
            var eventItem = Context.EventItems.First();
            eventItem.UpdateEventItem(new EventItemCommand.UpdateEventItem
            {
                Name = eventName,
                Id = eventItem.Id,
                EventOrganizer = "Son",
                EventTime = new DateTime(2020, 09, 16),
                Location = "Van",
                Members = new List<string> { "Johnson" }
            });

            var result = _testee.Update(eventItem);

            result.Should().BeOfType<EventItem>();
            result.Name.Should().Be(eventName);
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _CalendarContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new EventItem())).Should().ThrowAsync<Exception>().WithMessage("entity could not be saved: Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateEventItemAsync_WhenEventItemIsNotNull_ShouldReturnEventItem()
        {
            var result = await _testee.AddAsync(_newEventItem);

            result.Should().BeOfType<EventItem>();
        }

        [Fact]
        public async void CreateEventItemAsync_WhenEventItemIsNotNull_ShouldShouldAddEventItem()
        {
            var eventItemCount = Context.EventItems.Count();

            await _testee.AddAsync(_newEventItem);
            await _UnitOfWork.SaveAsync(default);

            Context.EventItems.Count().Should().Be(eventItemCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _CalendarContext.Set<EventItem>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAll()).Should().Throw<Exception>();
        }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.Update(null)).Should().Throw<ArgumentNullException>();
        }
    }
}
