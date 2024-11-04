using Infrastructure.MediatR.Events.Commands.CreateEvent;
using Infrastructure.MediatR.Events.Commands.DeleteEvent;
using Infrastructure.MediatR.Events.Commands.UpdateEvent;
using Infrastructure.MediatR.Events.Queries;
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
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetEventQuery { EventId = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new { Success = true, Event = result });
        }
        catch (Exception ex)
        {
            return NotFound(new { Success = false, Message = ex.Message });
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Event created successfully", EventId = result });
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventCommand command, CancellationToken cancellationToken)
    {
        if (id != command.EventId)
        {
            return BadRequest("Event ID in URL does not match ID in request body.");
        }
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Event updated successfully", EventId = result });
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new DeleteEventCommand { EventId = id }, cancellationToken);
            if (result)
            {
                return Ok(new { Message = "Event deleted successfully", EventId = id });
            }
            else
            {
                return NotFound(new { Success = false, Message = "Event not found" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, Message = ex.Message });
        }
    }

}
