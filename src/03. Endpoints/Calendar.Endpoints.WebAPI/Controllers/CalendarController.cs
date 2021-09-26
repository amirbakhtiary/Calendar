using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.RemoveEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem;
using Calendar.Core.Domain;
using Calendar.Endpoints.WebAPI.ViewModels;
using Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemById;
using Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemByOrganizer;
using Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItemOrderByTime;
using Calendar.Infrastructure.Data.Sql.Queries.EventItemAggregate.GetEventItems;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Calendar.Endpoints.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : BaseApiController
    {
        private readonly ILogger<CalendarController> _logger;

        public CalendarController(ILogger<CalendarController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<GetEventItemsDto>> GetEventItems()
        {
            return await Mediator.Send(new GetEventItemsQuery());
        }

        [HttpGet("{id}")]
        public async Task<GetEventItemByIdDto> GetEventItem(Guid id)
        {
            var eventitem = await Mediator.Send(new GetEventItemByIdQuery(id));

            return eventitem;
        }

        [HttpGet]
        [Route("query")]
        public async Task<IEnumerable<GetEventItemByOrganizerDto>> GetEventItemsByOrganizer(string eventOrganizer)
        {
            return await Mediator.Send(new GetEventItemByOrganizerQuery(eventOrganizer));
        }

        [HttpGet]
        [Route("sort")]
        public async Task<IEnumerable<GetEventItemOrderByTimeDto>> GetEventItemOrderByTime()
        {
            return await Mediator.Send(new GetEventItemOrderByTimeQuery());
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddNewEventItemDto), (int)HttpStatusCode.Created)]
        public async Task<ActionResult> CreateEvent(AddEventItemModel addEventItem)
        {
            var eventItem = await Mediator.Send(new AddNewEventItemCommand
            {
                Name = addEventItem.Name,
                EventTime = addEventItem.Time,
                EventOrganizer = addEventItem.EventOrganizer,
                Location = addEventItem.Location,
                Members = addEventItem.Members
            });

            return Created(Url.Action(nameof(GetEventItem), new { id = eventItem.Id }), eventItem);
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(UpdateEventItemDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateEvent(Guid id, UpdateEventItemModel editEventItem)
        {
            var eventUpdateResult = await Mediator.Send(new UpdateEventItemCommand
            {
                EventOrganizer = editEventItem.EventOrganizer,
                EventTime = editEventItem.Time,
                Id = id,
                Location = editEventItem.Location,
                Members = editEventItem.Members,
                Name = editEventItem.Name
            });
            if (!eventUpdateResult.IsSuccess)
                return NotFound();

            return Ok(eventUpdateResult.Data);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await Mediator.Send(new RemoveEventItemCommand { Id = id });
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
