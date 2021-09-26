using AutoMapper;
using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem;
using ViewModel = Calendar.Endpoints.WebAPI.ViewModels;

namespace Calendar.Endpoints.WebAPI.Extentions.Mapper
{
    public class DataProfile : Profile
    {
        public DataProfile()
        {
            CreateMap<ViewModel.AddEventItemModel, AddNewEventItemCommand>().ConstructUsing(au => new AddNewEventItemCommand
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
        }
    }
}
