using Calendar.Core.Domain;
using Calendar.Core.Domain.Commons;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem
{
    public class UpdateEventItemCommandHandler : IRequestHandler<UpdateEventItemCommand, UpdateEventItemDto>
    {
        private readonly ICalendarRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventItemCommandHandler(ICalendarRepository eventItemRepository,
            IUnitOfWork unitOfWork)
        {
            _eventItemRepository = eventItemRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateEventItemDto> Handle(UpdateEventItemCommand request, CancellationToken cancellationToken)
        {
            var eventItem = await _eventItemRepository.GetByIdAsync(request.Id);
            if (eventItem == null)
                return UpdateEventItemDto.Faild();

            eventItem.UpdateEventItem(new EventItemCommand.UpdateEventItem
            {
                Name = request.Name,
                Members = request.Members,
                EventOrganizer = request.EventOrganizer,
                EventTime = request.Time,
                Location = request.Location
            });

            await _unitOfWork.SaveAsync(cancellationToken);

            return UpdateEventItemDto.Success(request);
        }
    }
}
