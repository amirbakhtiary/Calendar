using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Calendar.AplicationService.Commands.EventItemAggregate.RemoveEventItem
{
    public class RemoveEventItemCommandHandler : IRequestHandler<RemoveEventItemCommand, bool>
    {
        private readonly IEventItemRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEventItemCommandHandler(IEventItemRepository eventItemRepository,
            IUnitOfWork unitOfWork)
        {
            _eventItemRepository = eventItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(RemoveEventItemCommand request, CancellationToken cancellationToken)
        {
            var eventItem = await _eventItemRepository.GetEventItemByIdAsync(request.Id,cancellationToken);
            if (eventItem == null)
                return false;

            _eventItemRepository.Remove(eventItem);
            await _unitOfWork.SaveAsync(cancellationToken);
            return true;
        }
    }
}
