using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem
{
    public class UpdateEventItemCommandHandler : IRequestHandler<UpdateEventItemCommand, UpdateEventItemDto>
    {
        private readonly IEventItemRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEventItemCommandHandler(IEventItemRepository eventItemRepository,
            IUnitOfWork unitOfWork)
        {
            _eventItemRepository = eventItemRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdateEventItemDto> Handle(UpdateEventItemCommand request, CancellationToken cancellationToken)
        {
            var eventItem = await _eventItemRepository.GetEventItemByIdAsync(request.Id, cancellationToken);
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
            _eventItemRepository.Update(eventItem);

            await _unitOfWork.SaveAsync(cancellationToken);

            return UpdateEventItemDto.Success(request);
        }
    }
}
