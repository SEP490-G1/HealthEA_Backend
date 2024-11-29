using Infrastructure.MediatR.Events.Commands.CreateEvent;
using Infrastructure.MediatR.Events.Commands.DeleteEvent;
using Infrastructure.MediatR.Events.Commands.UpdateEvent;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsWithUserController : ControllerBase
{
    private readonly IMediator _mediator;
    public EventsWithUserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEvent(Guid id, [FromBody] UpdateEventWithUserCommand command, CancellationToken cancellationToken)
    {
        if (id != command.EventId)
        {
            return BadRequest("Event ID in URL does not match ID in request body.");
        }
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new { Success = true, EventId = result });
    }
    [HttpPut("update-status")]
    public async Task<IActionResult> UpdateEventStatus([FromBody] UpdateEventWithStatus command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result)
            {
                return Ok(new { Success = true, Message = "Status updated successfully." });
            }

            return BadRequest(new { Success = false, Message = "Failed to update status." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, Message = ex.Message });
        }
    }
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventWithUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new DeleteEventWithUserCommand { EventId = id }, cancellationToken);
            return Ok(new { Success = true, Deleted = result });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, Message = ex.Message });
        }
    }

}
