using Domain.Interfaces.IRepositories;
using Domain.Interfaces.IServices;
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
    private readonly IUserClaimsService service;
    private readonly IDoctorRepository doctorRepository;

	public SchedulesController(IMediator mediator, IUserClaimsService service, IDoctorRepository doctorRepository)
	{
		_mediator = mediator;
		this.service = service;
		this.doctorRepository = doctorRepository;
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

	[HttpGet("is-user/{id}")]
	public async Task<IActionResult> IsUserTheDoctor(Guid id)
	{
		var userId = service.ClaimId(User);
		var doctor = await doctorRepository.GetDoctorByUserIdAsync(userId);
		if (doctor == null)
		{
			return BadRequest(new { Success = false, Message = "User is not a doctor!" });
		}
        if (doctor.Id != id)
        {
			return BadRequest(new { Success = false, Message = "User is not this doctor!" });
		}
		return Ok(new { Success = true, Message = "User is this doctor." });
	}

	[HttpPost]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleCommand command)
    {
        var userId = service.ClaimId(User);
        var doctor = await doctorRepository.GetDoctorByUserIdAsync(userId);
        if (doctor == null)
        {
			return BadRequest(new { Success = false, Message = "User is not a doctor!" });
		}
        command.DoctorId = doctor.Id;
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
