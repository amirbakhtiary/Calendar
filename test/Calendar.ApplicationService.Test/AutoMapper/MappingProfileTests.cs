using AutoMapper;
using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem;
using Calendar.Core.Domain.Entities;
using Calendar.Endpoints.WebAPI.Extentions.Mapper;
using Calendar.Endpoints.WebAPI.Models;
using FluentAssertions;
using System;
using Xunit;

namespace Calendar.Endpoints.WebAPI.Test.AutoMapper
{
    public class MappingProfileTests
    {
        private readonly CreateEventItemModel _createEventItemModel;
        private readonly UpdateEventItemModel _updateEventItemModel;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _createEventItemModel = new CreateEventItemModel
            {
                Name = "Event",
                Time = DateTime.Now.AddDays(2),
                Location = "Alva",
                EventOrganizer = "John",
                Members = { "Rj", "Hj" }
            };

            _updateEventItemModel = new UpdateEventItemModel
            {
                Name = "Event",
                Time = DateTime.Now.AddDays(2),
                Location = "Alva",
                EventOrganizer = "John",
                Members = { "Rj", "Hj" }
            };
        }

        [Fact]
        public void Map_AddNewEventItemCommand_CreateEventItemModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<CreateEventItemModel, AddNewEventItemCommand>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_UpdateEventItemCommand_UpdateEventItemModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<UpdateEventItemModel, UpdateEventItemCommand>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_EventItem_EventItemModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<EventItem, EventItemModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreateEventItemModel_EventItem()
        {
            var eventItem = _mapper.Map<AddNewEventItemCommand>(_createEventItemModel);

            eventItem.Name.Should().Be(_createEventItemModel.Name);
            eventItem.Time.Should().Be(_createEventItemModel.Time);
            eventItem.EventOrganizer.Should().Be(_createEventItemModel.EventOrganizer);
            eventItem.Location.Should().Be(_createEventItemModel.Location);
            eventItem.Members.Should().BeEquivalentTo(_createEventItemModel.Members);
        }

        [Fact]
        public void Map_UpdateEventItemModel_EventItem()
        {
            var eventItem = _mapper.Map<UpdateEventItemCommand>(_updateEventItemModel);

            eventItem.Name.Should().Be(_updateEventItemModel.Name);
            eventItem.Time.Should().Be(_updateEventItemModel.Time);
            eventItem.EventOrganizer.Should().Be(_updateEventItemModel.EventOrganizer);
            eventItem.Location.Should().Be(_updateEventItemModel.Location);
            eventItem.Members.Should().BeEquivalentTo(_updateEventItemModel.Members);
        }
    }
}
