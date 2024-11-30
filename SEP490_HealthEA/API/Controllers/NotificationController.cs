using Domain.Interfaces.IServices;
using Infrastructure.MediatR.DeviceToken;
using Infrastructure.MediatR.Notices;
using Infrastructure.MediatR.Schedules.Queries;
using Infrastructure.SQLServer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly FirebaseNotificationService _firebaseService;
    private readonly IUserClaimsService userClaimsService;
    private readonly SqlDBContext context; //Sorry


	public NotificationController(FirebaseNotificationService firebaseService, IMediator mediator, IUserClaimsService userClaimsService, SqlDBContext context)
	{
		_firebaseService = firebaseService;
		_mediator = mediator;
		this.userClaimsService = userClaimsService;
		this.context = context;
	}
	[HttpGet]
    [Authorize]
    public async Task<IActionResult> GetNotices([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var userId = userClaimsService.ClaimId(User);
        var query = new GetListNoticeQuery
        {
            UserId = userId,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var notices = await _mediator.Send(query);

        return Ok(notices);
    }

    [HttpGet("any")]
    [Authorize]
    public async Task<IActionResult> GetIfThereAreNewNotices()
    {
		var userId = userClaimsService.ClaimId(User);
        var result = await context.Notices.AnyAsync(x => x.RecipientId == userId && !x.HasViewed);
        return Ok(new {
            result
        });
	}

	[HttpGet("readAll")]
	[Authorize]
	public async Task<IActionResult> MarkNotificationsAsRead()
	{
		var userId = userClaimsService.ClaimId(User);
        var result = await context.Notices
            .Where(n => n.RecipientId == userId)
            .ExecuteUpdateAsync(n => n.SetProperty(n => n.HasViewed, n => true));
		return Ok();
	}

	[HttpPost("test-notice")]
    public async Task<IActionResult> SendNotification([FromBody] string deviceToken)
    {
        string title = "Thông báo mới!";
        string body = "Bạn có một thông báo mới.";

        try
        {
            await _firebaseService.SendNotificationAsync(deviceToken, title, body);
            return Ok("Notification sent successfully!");
        }
        catch (Exception ex)
        {
            return BadRequest($"Failed to send notification: {ex.Message}");
        }
    }
    [HttpPost("register-token")]
    [Authorize]
    public async Task<IActionResult> RegisterDeviceToken([FromBody] RegisterDeviceTokenCommand command)
    {
        var userId = userClaimsService.ClaimId(User);
        command.UserId = userId;
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateNotice([FromBody] CreateNoticeCommand command)
    {
        var userId = userClaimsService.ClaimId(User);
        command.UserId = userId;
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }


}

