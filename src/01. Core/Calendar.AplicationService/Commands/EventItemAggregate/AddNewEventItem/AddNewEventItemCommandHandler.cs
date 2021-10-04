using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Calendar.Core.Domain.Entities;
using Calendar.Infrastructure.Data.Sql.Repository;
using Calendar.Infrastructure.Data.Sql.UnitOfWork;
using MediatR;

namespace Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem
{
    public class AddNewEventItemCommandHandler : IRequestHandler<AddNewEventItemCommand, AddNewEventItemDto>
    {
        private readonly IEventItemRepository _eventItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddNewEventItemCommandHandler(IEventItemRepository eventItemRepository,
            IUnitOfWork unitOfWork)
        {
            _eventItemRepository = eventItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AddNewEventItemDto> Handle(AddNewEventItemCommand request, CancellationToken cancellationToken)
        {
            var result = await _eventItemRepository.AddAsync(EventItem.AddEventItem(new EventItemCommand.AddNewEventItem
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
