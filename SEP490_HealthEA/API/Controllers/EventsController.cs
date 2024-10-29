using Infrastructure.MediatR.Events.Commands.CreateEvent;
using Infrastructure.MediatR.Events.Commands.DeleteEvent;
using Infrastructure.MediatR.Events.Commands.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    public EventsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command, CancellationToken cancellationToken)
    {
        if (id != command.EventId)
        {
            return BadRequest("Event ID in URL does not match ID in request body.");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command,cancellationToken);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteEventCommand { EventId = id }, cancellationToken);
        return Ok(result);
    }
}
