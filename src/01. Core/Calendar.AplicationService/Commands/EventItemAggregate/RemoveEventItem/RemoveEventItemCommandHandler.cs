using Calendar.Core.Domain;
using Calendar.Core.Domain.Commons;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Commands.EventItemAggregate.RemoveEventItem
{
    public class RemoveEventItemCommandHandler : IRequestHandler<RemoveEventItemCommand, bool>
    {
        private readonly ICalendarRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEventItemCommandHandler(ICalendarRepository eventItemRepository,
            IUnitOfWork unitOfWork)
        {
            _eventItemRepository = eventItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveEventItemCommand request, CancellationToken cancellationToken)
        {
            var eventItem = await _eventItemRepository.GetByIdAsync(request.Id);
            if (eventItem == null)
                return false;

            _eventItemRepository.DeleteItem(eventItem);
            await _unitOfWork.SaveAsync(cancellationToken);
            return true;
        }
    }
}
