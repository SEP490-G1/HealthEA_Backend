using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Infrastructure.MediatR.Events.Commands.CreateEvent;
using Infrastructure.MediatR.Events.Commands.DeleteEvent;
using Infrastructure.MediatR.Events.Commands.UpdateEvent;
using Infrastructure.MediatR.Events.Queries;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserClaimsService userClaimsService;
    public EventsController(IMediator mediator, IUserClaimsService userClaimsService)
    {
        _mediator = mediator;
        this.userClaimsService = userClaimsService;
    }
    [HttpGet("eventList")]
    public async Task<IActionResult> GetEvents([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        var userId = userClaimsService.ClaimId(User);
        var query = new GetEventsQuery
        {
            UserId = userId,
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
            return NotFound(new { Message = "No events found in the specified date range or user has not created any events" });
        }
    }
    //k can
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
            var userId = userClaimsService.ClaimId(User);
            var query = new GeEventWithIDQuery { EventId = id, UserId = userId };
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
        var userId = userClaimsService.ClaimId(User);
        command.UserId = userId;
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new { Message = "Event created successfully", OriginEventId = result });
    }
    [HttpPut("all/{id}")]
    public async Task<IActionResult> UpdateAllEvent(Guid id, [FromBody] UpdateAllEventCommand command, CancellationToken cancellationToken)
    {
        var userId = userClaimsService.ClaimId(User);
        command.UserId = userId;
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
        var userId = userClaimsService.ClaimId(User);
        command.UserId = userId;
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
            var userId = userClaimsService.ClaimId(User);
            //command.UserId = userId;
            var result = await _mediator.Send(new DeleteEventCommand { EventId = id, UserId = userId }, cancellationToken);
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
            var userId = userClaimsService.ClaimId(User);
            //command.UserId = userId;
            var result = await _mediator.Send(new DeleteAllEventCommand { OriginalEventId = originalEventId, UserId = userId }, cancellationToken);
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
