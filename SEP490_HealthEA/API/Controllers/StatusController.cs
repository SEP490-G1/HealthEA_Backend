using Infrastructure.MediatR.Events.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // API cho sự kiện Upcoming
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingEvents(CancellationToken cancellationToken)
        {
            var query = new GetEventWithUpcoming();
            var events = await _mediator.Send(query, cancellationToken);
            return Ok(new { Success = true, Data = events });
        }

        // API cho sự kiện Pending
        [HttpGet("pending")]
        public async Task<IActionResult> GetPendingEvents(CancellationToken cancellationToken)
        {
            var query = new GetEventWithPending();
            var events = await _mediator.Send(query, cancellationToken);
            return Ok(new { Success = true, Data = events });
        }

        // API cho sự kiện Recurring
        [HttpGet("recurring")]
        public async Task<IActionResult> GetRecurringEvents(CancellationToken cancellationToken)
        {
            var query = new GetEventWithRecurring();
            var events = await _mediator.Send(query, cancellationToken);
            return Ok(new { Success = true, Data = events });
        }

        // API cho sự kiện Past
        [HttpGet("past")]
        public async Task<IActionResult> GetPastEvents(CancellationToken cancellationToken)
        {
            var query = new GetEventWithPast();
            var events = await _mediator.Send(query, cancellationToken);
            return Ok(new { Success = true, Data = events });
        }

        // API cho sự kiện Cancelled
        [HttpGet("cancelled")]
        public async Task<IActionResult> GetCancelledEvents(CancellationToken cancellationToken)
        {
            var query = new GetEventWithCancel();
            var events = await _mediator.Send(query, cancellationToken);
            return Ok(new { Success = true, Data = events });
        }
    }
}
