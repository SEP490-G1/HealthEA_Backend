using Domain.Common;
using Domain.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly INotificationFactory _notificationFactory;
        public TestsController(INotificationFactory _notificationFactor)
        {
            _notificationFactory = _notificationFactor;
        }
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var body = $"test message";
            var notificationService = _notificationFactory.CreateNotificationService(NotificationType.Email);
            var customerEmail = "manhvv15@gmail.com";
            await notificationService.SendNotification(customerEmail, "Order Created", body);
            return Ok();
        }
    }
}
