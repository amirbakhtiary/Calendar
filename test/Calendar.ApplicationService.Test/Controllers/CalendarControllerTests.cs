using AutoMapper;
using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem;
using Calendar.AplicationService.Queries.EventItemAggregate;
using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItems;
using Calendar.Core.Domain.Entities;
using Calendar.Endpoints.WebAPI.Controllers;
using Calendar.Endpoints.WebAPI.Models;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace Calendar.Endpoints.WebAPI.Test.Controllers
{
    public class CalendarControllerTests
    {
        private readonly IMediator _mediator;
        private readonly CalendarController _testee;
        private readonly CreateEventItemModel _createEventItemModel;
        private readonly UpdateEventItemModel _updateEventItemModel;
        private readonly Guid _id = Guid.Parse("5224ed94-6d9c-42ec-ba93-dfb11fe68931");

        public CalendarControllerTests()
        {
            var _mapper = A.Fake<IMapper>();
            var _logger = A.Fake<ILogger<CalendarController>>();
            _mediator = A.Fake<IMediator>();
            _testee = new CalendarController(_logger, _mapper, _mediator);

            _createEventItemModel = new CreateEventItemModel
            {
                Name = "Event",
                Time = DateTime.Now.AddDays(2),
                Location = "Alva",
                EventOrganizer = "John",
                Members = new List<string> { "Rj", "Hj" }
            };

            _updateEventItemModel = new UpdateEventItemModel
            {
                Name = "Event",
                Time = DateTime.Now.AddDays(2),
                Location = "Alva",
                EventOrganizer = "John",
                Members = new List<string> { "Rj", "Hj" }
            };

            var eventItemDto = new List<EventItemDto>
            {
                new EventItemDto
                {
                    Id = _id,
                    Name = "Event",
                    Time = DateTime.Now.AddDays(2),
                    Location = "Alva",
                    EventOrganizer = "John",
                    Members = new List<string>{ "Rj", "Hj" }
                },
                new EventItemDto
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    Name = "Meeting",
                    Time = DateTime.Now.AddDays(2),
                    Location = "Berlin",
                    EventOrganizer = "HR",
                    Members = new List<string>{ "IT", "HK" }
                }
            };

            var _addNewEventItemCommand = new AddNewEventItemCommand
            {
                Name = "Event",
                Time = DateTime.Now.AddDays(2),
                Location = "Alva",
                EventOrganizer = "John",
                Members = new List<string> { "Rj", "Hj" }
            };

            var _createEventItemDto = new AddNewEventItemDto
            {
                Name = "Event",
                Time = DateTime.Now.AddDays(2),
                Location = "Alva",
                EventOrganizer = "John",
                Members = new List<string> { "Rj", "Hj" }
            };

            var _updateEventItemDto = new UpdateEventItemDto
            {
                Data = new UpdateEventItemCommand
                {
                    Id = _id,
                    Name = "Event",
                    Time = DateTime.Now.AddDays(2),
                    Location = "Alva",
                    EventOrganizer = "John",
                    Members = new List<string> { "Rj", "Hj" }
                }
            };

            var eventItems = new List<EventItem>
            {
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Id = _id,
                    Name = "Event",
                    EventTime = DateTime.Now.AddDays(2),
                    Location = "Alva",
                    EventOrganizer = "John",
                    Members = new List<string>{ "Rj", "Hj" }
                }),
                EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    Name = "Meeting",
                    EventTime = DateTime.Now.AddDays(2),
                    Location = "Berlin",
                    EventOrganizer = "HR",
                    Members = new List<string>{ "IT", "HK" }
                })
            };

            A.CallTo(() => _mapper.Map<AddNewEventItemCommand>(A<CreateEventItemModel>._)).Returns(_addNewEventItemCommand);

            A.CallTo(() => _mediator.Send(A<AddNewEventItemCommand>._, default)).Returns(_createEventItemDto);
            A.CallTo(() => _mediator.Send(A<UpdateEventItemCommand>._, default)).Returns(_updateEventItemDto);
            A.CallTo(() => _mediator.Send(A<GetEventItemsQuery>._, default)).Returns(eventItemDto);
        }

        [Theory]
        [InlineData("CreateEventItemAsync: eventItem is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<AddNewEventItemCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.CreateEvent(_createEventItemModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("UpdateEventItemAsync: eventItem is null")]
        [InlineData("No eventItem with this id found")]
        public async void Put_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<UpdateEventItemCommand>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.UpdateEvent(_id, _updateEventItemModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Get_ShouldReturnEventItems()
        {
            var result = await _testee.GetEventItems();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<EventItemDto>>();
            result.Value.Count().Should().Be(2);
        }

        [Theory]
        [InlineData("EventItems could not be loaded")]
        public async void Get_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<GetEventItemsQuery>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.GetEventItems();

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Post_ShouldReturnEventItem()
        {
            var result = await _testee.CreateEvent(_createEventItemModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<AddNewEventItemDto>();
            result.Value.Id.Should().Be(_id);
        }

        [Fact]
        public async void Put_ShouldReturnEventItem()
        {
            var result = await _testee.UpdateEvent(_id, _updateEventItemModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<UpdateEventItemDto>();
            result.Value.Data.Id.Should().Be(_id);
        }
    }
}
