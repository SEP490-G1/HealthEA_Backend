using Infrastructure.MediatR.Events.Commands.CreateEvent;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand command, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(command,cancellationToken);
        return Ok(result);
    } 
}
