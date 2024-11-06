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
    [HttpGet("eventList")]
    public async Task<IActionResult> GetEvents([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var query = new GetEventsQuery
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var events = await _mediator.Send(query);

        if (events.Count > 0)
        {
            return Ok(new { Events = events });
        }
        else
        {
            return NotFound(new { Message = "No events found in the specified date range." });
        }
    }
    [HttpGet("get-event")]
    public async Task<IActionResult> GetEvent([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, CancellationToken cancellationToken)
    {
        var query = new GetEventAllQuery
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GeEventWithDateQuery { EventId = id };
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
        return Ok(new { Message = "Event created successfully", OriginEventId = result });
    }
    [HttpPut("all/{id}")]
    public async Task<IActionResult> UpdateAllEvent(Guid id, [FromBody] UpdateAllEventCommand command, CancellationToken cancellationToken)
    {
        if (id != command.EventId)
        {
            return BadRequest("Event ID in URL does not match ID in request body.");
        }
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Event updated successfully", EventId = result });
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
    [HttpDelete("all/{originalEventId}")]
    public async Task<IActionResult> DeleteAllEvent(Guid originalEventId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new DeleteAllEventCommand { OriginalEventId = originalEventId }, cancellationToken);
            if (result)
            {
                return Ok(new { Message = "All events deleted successfully", OriginalEventId = originalEventId });
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
