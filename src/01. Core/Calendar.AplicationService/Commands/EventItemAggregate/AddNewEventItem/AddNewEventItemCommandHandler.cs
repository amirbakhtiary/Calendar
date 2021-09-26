using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Calendar.Core.Domain;
using Calendar.Core.Domain.Commons;
using MediatR;

namespace Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem
{
    public class AddNewEventItemCommandHandler : IRequestHandler<AddNewEventItemCommand, AddNewEventItemDto>
    {
        private readonly IRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddNewEventItemCommandHandler(IRepository eventItemRepository,
            IUnitOfWork unitOfWork)
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
                EventTime = request.EventTime,
                Location = request.Location
            }));
            await _unitOfWork.SaveAsync(cancellationToken);

            return new AddNewEventItemDto
            {
                Id = result.Id,
                Name = request.Name,
                Location = request.Location,
                EventTime = request.EventTime,
                EventOrganizer = request.EventOrganizer,
                Members = request.Members
            };
        }
    }
}
