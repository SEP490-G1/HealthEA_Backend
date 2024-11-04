using Infrastructure.Notification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly FirebaseService _firebaseService;

    public NotificationController(FirebaseService firebaseService)
    {
        _firebaseService = firebaseService;
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
}

