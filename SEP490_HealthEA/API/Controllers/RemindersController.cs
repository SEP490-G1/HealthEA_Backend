using Infrastructure.MediatR.Reminders.Commands.CreateReminder;
using Infrastructure.MediatR.Reminders.Commands.DeleteReminder;
using Infrastructure.MediatR.Reminders.Commands.UpdateReminder;
using Infrastructure.MediatR.Reminders.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RemindersController : ControllerBase
{
    private readonly IMediator _mediator;

    public RemindersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // Create Reminder
    [HttpPost]
    public async Task<IActionResult> CreateReminder([FromBody] CreateReminderCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new { Success = true, Message = "Reminder created successfully", ReminderId = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEvent([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var query = new GetReminderQuery { ReminderId = id };
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(new { Success = true, Event = result });
        }
        catch (Exception ex)
        {
            return NotFound(new { Success = false, Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReminder(Guid id, [FromBody] UpdateReminderCommand command, CancellationToken cancellationToken)
    {
        if (id != command.ReminderId)
        {
            return BadRequest(new { Success = false, Message = "Reminder ID in URL does not match ID in request body." });
        }

        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new { Success = true, Message = "Reminder updated successfully", ReminderId = result });
        }
        catch (Exception ex)
        {
            return NotFound(new { Success = false, Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReminder(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new DeleteReminderCommand { ReminderId = id }, cancellationToken);
            if (result)
            {
                return Ok(new { Success = true, Message = "Reminder deleted successfully" });
            }
            else
            {
                return NotFound(new { Success = false, Message = "Reminder not found" });
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = false, Message = ex.Message });
        }
    }
}
