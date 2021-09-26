using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calendar.Core.Domain;
using Calendar.Core.Domain.Commons;
using MediatR;

namespace Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem
{
    public class AddNewEventItemCommandHandler : IRequestHandler<AddNewEventItemCommand, AddNewEventItemDto>
    {
        private readonly ICalendarRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddNewEventItemCommandHandler(ICalendarRepository eventItemRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _eventItemRepository = eventItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddNewEventItemDto> Handle(AddNewEventItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _eventItemRepository.AddItemAsync(EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
            {
                Name = request.Name,
                Members = request.Members,
                EventOrganizer = request.EventOrganizer,
                EventTime = request.Time,
                Location = request.Location
            }));
            await _unitOfWork.SaveAsync(cancellationToken);

            return new AddNewEventItemDto
            {
                Id = result.Id,
                Name = request.Name,
                Location = request.Location,
                Time = request.Time,
                EventOrganizer = request.EventOrganizer,
                Members = request.Members
            };
        }
    }
}
