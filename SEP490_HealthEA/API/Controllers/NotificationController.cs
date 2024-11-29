using Domain.Interfaces.IServices;
using Infrastructure.MediatR.DeviceToken;
using Infrastructure.MediatR.Notices;
using Infrastructure.MediatR.Schedules.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly FirebaseNotificationService _firebaseService;
    private readonly IUserClaimsService userClaimsService;


    public NotificationController(FirebaseNotificationService firebaseService, IMediator mediator, IUserClaimsService userClaimsService)
    {
        _firebaseService = firebaseService;
        _mediator = mediator;
        this.userClaimsService = userClaimsService;
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

