using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheConference.InfoBooth.Core.Sessions.Handlers;

namespace TheConference.Shared.Web.RestApi.Controllers {
    [Produces("application/json")]
    [Route("api/Sessions")]
    public class SessionsController : Controller {
        private readonly IMediator _mediator;

        public SessionsController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get() {
            var response = _mediator.Send(new ListSessionsQuery());
            return new JsonResult(response);
        }

        [HttpGet]
        [Route("{slug}")]
        public IActionResult Get(string slug) {
            var response = _mediator.Send(new GetSessionBySlugQuery(slug));
            return new JsonResult(response);
        }
    }
}