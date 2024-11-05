using Infrastructure.MediatR.DeviceToken;
using Infrastructure.MediatR.Notices;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly FirebaseNotificationService _firebaseService;

    public NotificationController(FirebaseNotificationService firebaseService, IMediator mediator)
    {
        _firebaseService = firebaseService;
        _mediator = mediator;
    }

    [HttpPost("send")]
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
    public async Task<IActionResult> RegisterDeviceToken([FromBody] RegisterDeviceTokenCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }
    [HttpPost("create-notice")]
    public async Task<IActionResult> CreateNotice([FromBody] CreateNoticeCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(new { message = result });
    }


}

