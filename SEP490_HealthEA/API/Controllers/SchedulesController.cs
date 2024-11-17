using Infrastructure.MediatR.Schedules.Commands.CreateSchedule;
using Infrastructure.MediatR.Schedules.Commands.DeleteSchedule;
using Infrastructure.MediatR.Schedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SchedulesController : ControllerBase
{
    private readonly IMediator _mediator;

    public SchedulesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpGet("by-day")]
    public async Task<IActionResult> GetSchedulesByDay([FromQuery] DateTime date, [FromQuery] Guid? doctorId)
    {
        var query = new GetScheduleByDayQuery
        {
            DoctorId = doctorId,
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
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
    {
        try
        {
            var schedules = await _mediator.Send(command);
            return Ok(new { Success = true, Message = "Schedule created successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
    [HttpDelete("{scheduleId}")]
    public async Task<IActionResult> DeleteSchedule(Guid scheduleId)
    {
        try
        {
            var command = new DeleteScheduleCommand { ScheduleId = scheduleId };
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("Lịch đã được xóa thành công.");
            }

            return BadRequest("Xóa lịch thất bại.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
