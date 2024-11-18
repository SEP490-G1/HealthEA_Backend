using Domain.Interfaces.IServices;
using Infrastructure.MediatR.Schedules.Commands.CreateSchedule;
using Infrastructure.MediatR.Schedules.Commands.DeleteSchedule;
using Infrastructure.MediatR.Schedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class SchedulesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserClaimsService userClaimsService;


    public SchedulesController(IMediator mediator, IUserClaimsService userClaimsService)
    {
        _mediator = mediator;
        this.userClaimsService = userClaimsService;
    }
    
    [HttpGet("by-day")]
    [Authorize(Roles = "DOCTOR")]
    public async Task<IActionResult> GetSchedulesByDay([FromQuery] DateTime date, [FromQuery] Guid? doctorId)
    {
        var userId = userClaimsService.ClaimId(User);
        var userRole = userClaimsService.ClaimRole(User);
        var query = new GetScheduleByDayQuery
        {
            UserId = userId,
            Date = date
        };

        var schedules = await _mediator.Send(query);
 
        if (schedules == null || schedules.Count == 0)
        {
            return NotFound("Không có lịch hẹn cho ngày này.");
        }

        return Ok(schedules);
    }
    [HttpPost]
    [Authorize(Roles = "DOCTOR")]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
    {
        try
        {
            var userId = userClaimsService.ClaimId(User);
            var userRole = userClaimsService.ClaimRole(User);
            command.UserId = userId;
            var schedules = await _mediator.Send(command);
            return Ok(new { Success = true, Message = "Schedule created successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
    [HttpDelete("{scheduleId}")]
    [Authorize(Roles = "DOCTOR")]
    public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
    {
        try
        {
            var userId = userClaimsService.ClaimId(User);
            var userRole = userClaimsService.ClaimRole(User);
            var command = new DeleteScheduleCommand { ScheduleId = scheduleId, UserId = userId };
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("Schedule deleted successfully.");
            }

            return BadRequest("Schedule deleted failed.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
