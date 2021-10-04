using AutoMapper;
using Calendar.AplicationService.Commands.EventItemAggregate.AddNewEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.RemoveEventItem;
using Calendar.AplicationService.Commands.EventItemAggregate.UpdateEventItem;
using Calendar.AplicationService.Queries.EventItemAggregate;
using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemById;
using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemByOrganizer;
using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItemOrderByTime;
using Calendar.AplicationService.Queries.EventItemAggregate.GetEventItems;
using Calendar.Endpoints.WebAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Calendar.Endpoints.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CalendarController : BaseApiController
    {
        private readonly ILogger<CalendarController> _logger;
        private readonly IMapper _mapper;

        public CalendarController(ILogger<CalendarController> logger, IMapper mapper, IMediator mediator) : base(mediator)
        {
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Action to see all existing eventItems.
        /// </summary>
        /// <returns>Returns a list of all eventItems</returns>
        /// <response code="200">Returned if the eventItems were loaded</response>
        /// <response code="400">Returned if the eventItems couldn't be loaded</response>
        [ProducesResponseType(typeof(List<EventItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventItemDto>>> GetEventItems()
        {
            return await Mediator.Send(new GetEventItemsQuery());
        }

        /// <summary>
        /// Action to see eventItem by Id.
        /// </summary>
        /// <returns>Returns an eventItem by Id</returns>
        /// <response code="200">Returned if the eventItem were loaded</response>
        /// <response code="400">Returned if the eventItem couldn't be loaded</response>
        [ProducesResponseType(typeof(EventItemModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet("{id}")]
        public async Task<EventItemModel> GetEventItem(Guid id)
        {
            var eventitem = await Mediator.Send(new GetEventItemByIdQuery(id));
            var model = _mapper.Map<EventItemModel>(eventitem);
            return model;
        }

        /// <summary>
        /// Action to see eventItem by eventOrganizer.
        /// </summary>
        /// <returns>Returns an eventItem by eventOrganizer</returns>
        /// <response code="200">Returned if the eventItem were loaded</response>
        /// <response code="400">Returned if the eventItem couldn't be loaded</response>
        [ProducesResponseType(typeof(List<EventItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("query")]
        [HttpGet]
        public async Task<IEnumerable<EventItemDto>> GetEventItemsByOrganizer(string eventOrganizer)
        {
            return await Mediator.Send(new GetEventItemByOrganizerQuery(eventOrganizer));
        }

        /// <summary>
        /// Action to see eventItems order by time descending.
        /// </summary>
        /// <returns>Returns list of eventItems order by time descending</returns>
        /// <response code="200">Returned if the eventItem were loaded</response>
        /// <response code="400">Returned if the eventItem couldn't be loaded</response>
        [ProducesResponseType(typeof(List<EventItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("sort")]
        [HttpGet]
        public async Task<IEnumerable<EventItemDto>> GetEventItemOrderByTime()
        {
            return await Mediator.Send(new GetEventItemOrderByTimeQuery());
        }

        /// <summary>
        /// Action to create a new eventItem in the database.
        /// </summary>
        /// <param name="addEventItem">Model to create a new eventItem</param>
        /// <returns>Returns the created eventItem</returns>
        /// <response code="201">Returned if the eventItem was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the eventItem couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(typeof(AddNewEventItemDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost]
        public async Task<ActionResult<AddNewEventItemDto>> CreateEvent(CreateEventItemModel addEventItem)
        {
            var dataModel = _mapper.Map<AddNewEventItemCommand>(addEventItem);

            var eventItem = await Mediator.Send(dataModel);

            return Created(Url.Action(nameof(GetEventItem), new { id = eventItem.Id }), eventItem);
        }

        /// <summary>
        /// Action to update an existing eventItem
        /// </summary>
        /// <param name="id">Id to update an existing eventItem.</param>
        /// <param name="editEventItem">Model to update an existing eventItem.</param>
        /// <returns>Returns the updated eventItem</returns>
        /// <response code="200">Returned if the eventItem was updated</response>
        /// <response code="400">Returned if the model couldn't be parsed</response>
        /// <response code="404">Returned if the eventItem couldn't be found</response>
        /// <response code="422">Returned when the validation failed</response>
        [ProducesResponseType(typeof(UpdateEventItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut("{id}")]
        public async Task<ActionResult<UpdateEventItemDto>> UpdateEvent(Guid id, UpdateEventItemModel editEventItem)
        {
            var dataModel = _mapper.Map<UpdateEventItemCommand>(editEventItem);
            dataModel.Id = id;
            var eventUpdateResult = await Mediator.Send(dataModel);

            if (!eventUpdateResult.IsSuccess)
                return NotFound();

            return Ok(eventUpdateResult.Data);
        }

        /// <summary>
        /// Action to delete an existing eventItem
        /// </summary>
        /// <param name="id">Id to delete an existing eventItem.</param>
        /// <returns>Returns the status of action</returns>
        /// <response code="200">Returned if the eventItem was deleted</response>
        /// <response code="404">Returned if the eventItem couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var result = await Mediator.Send(new RemoveEventItemCommand { Id = id });
            if (!result)
                return NotFound();

            return Ok();
        }
    }
}
