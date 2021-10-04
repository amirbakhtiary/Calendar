using AutoMapper;
using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem;
using Calendar.Core.Domain.Entities;
using System.Linq;
using ViewModel = Calendar.Endpoints.WebAPI.Models;

namespace Calendar.Endpoints.WebAPI.Extentions.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ViewModel.CreateEventItemModel, AddNewEventItemCommand>().ConstructUsing(au => new AddNewEventItemCommand
            {
                EventOrganizer = au.EventOrganizer,
                Time = au.Time,
                Location = au.Location,
                Members = au.Members,
                Name = au.Name
            });

            CreateMap<ViewModel.UpdateEventItemModel, UpdateEventItemCommand>().ConstructUsing(au => new UpdateEventItemCommand
            {
                EventOrganizer = au.EventOrganizer,
                Time = au.Time,
                Location = au.Location,
                Members = au.Members,
                Name = au.Name
            });

            CreateMap<EventItem, ViewModel.EventItemModel>().ConvertUsing(au => new ViewModel.EventItemModel
            {
                Id = au.Id,
                EventOrganizer = au.EventOrganizer.Name,
                Time = au.EventTime,
                Location = au.Location.Name,
                Members = au.Members.Select(m => m.Name).ToList(),
                Name = au.Name
            });
        }
    }
}
