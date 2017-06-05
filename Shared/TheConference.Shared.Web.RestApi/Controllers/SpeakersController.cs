using MediatR;
using Microsoft.AspNetCore.Mvc;
using TheConference.InfoBooth.Core.Speakers.Handlers;

namespace TheConference.Shared.Web.RestApi.Controllers {
    [Produces("application/json")]
    [Route("api/Speakers")]
    public class SpeakersController : Controller {
        private readonly IMediator _mediator;

        public SpeakersController(IMediator mediator) {
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult Get() {
            var response = _mediator.Send(new ListSpeakersQuery());
            return new JsonResult(response);
        }

        [HttpGet]
        [Route("{slug}")]
        public IActionResult Get(string slug) {
            var response = _mediator.Send(new GetSpeakerBySlugQuery(slug));
            return new JsonResult(response);
        }

        [HttpGet]
        [Route("{slug}/sessions")]
        public IActionResult GetSessions(string slug) {
            var response = _mediator.Send(new GetSessionsBySpeakerSlugQuery(slug));
            return new JsonResult(response);
        }
    }
}
