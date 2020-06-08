using Doctrina.Application.Activities.Queries;
using Doctrina.ExperienceApi.Data;
using Doctrina.WebUI.ExperienceApi.Mvc.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Doctrina.WebUI.ExperienceApi.Controllers
{
    [Authorize]
    [RequiredVersionHeader]
    [Route("xapi/activities")]
    public class ActivitiesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ActivitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [HttpHead]
        public async Task<ActionResult> GetActivityDocumentAsync([FromQuery]Guid id, CancellationToken cancelToken = default)
        {
            var command = new GetActivityQuery() { ActivityId = new Iri(id.ToString()) };

            Activity activity = await _mediator.Send(command, cancelToken);

            if (activity == null)
            {
                return Ok(new Activity());
            }

            // TODO: Return only canonical that match accept-language header, or und

            return Ok(activity);
        }

        [HttpGet]
        [HttpHead]
        public async Task<ActionResult> GetActivityDocumentAsync([FromQuery]GetActivityQuery command, CancellationToken cancelToken = default)
        {
            Activity activity = await _mediator.Send(command, cancelToken);

            if (activity == null)
            {
                return Ok(new Activity());
            }

            // TODO: Return only canonical that match accept-language header, or und

            return Ok(activity);
        }
    }
}
