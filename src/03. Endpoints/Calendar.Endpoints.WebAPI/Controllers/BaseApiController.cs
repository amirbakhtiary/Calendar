using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Calendar.Endpoints.WebAPI.Controllers
{
    public abstract class BaseApiController : ControllerBase
    {
        //private IMediator _mediator;
        //protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        protected BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; private set; }
    }
}
