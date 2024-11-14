using Domain.Common.Exceptions;
using Domain.Interfaces.IServices;
using Domain.Models.Entities;
using Infrastructure.MediatR.Appoinment.Commands.CreateAppointment;
using Infrastructure.MediatR.Appoinment.Queries;
using Infrastructure.MediatR.Common;
using Infrastructure.MediatR.Reminders.Commands.CreateReminder;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserClaimsService service;

	public AppointmentsController(IMediator mediator, IUserClaimsService service)
	{
		_mediator = mediator;
		this.service = service;
	}
	[HttpGet]
    public async Task<ActionResult<PaginatedList<AppointmentDto>>> GetAppointmentWithPagination([FromQuery] GetAppointment query, CancellationToken cancellationToken)
    {
        return await _mediator.Send(query, cancellationToken);
    }
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAppointmentsByUserId(Guid userId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var query = new GetAppointmentByUserId
        {
            UserId = userId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _mediator.Send(query);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateReminder([FromBody] CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        command.UserId = service.ClaimId(User);
        Console.WriteLine(command.UserId);
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(new { Success = true, Message = "Appointment created successfully", AppointmentId = result});
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
    [HttpPost("approve/{appointmentId}")]
    public async Task<IActionResult> ApproveAppointment([FromBody] ApproveAppointmentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result)
            {
                return Ok(new { Success = true, Message = "Appointment approve successfully"});
            }
            else
            {
                return NotFound(new { Error = ErrorCode.APPOINTMENT_NOT_FOUND });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
    [HttpPost("reject/{appointmentId}")]
    public async Task<IActionResult> RejectAppointment([FromBody] RejectAppointmentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result)
            {
                return Ok(new { Success = true, Message = "Appointment rejected successfully", AppointmentId = result });
            }
            else
            {
                return NotFound(new { Error = ErrorCode.APPOINTMENT_NOT_FOUND });
            }
        }
        catch (Exception ex)
        {
            return BadRequest(new { Success = false, Message = ex.Message });
        }
    }
}
