using Domain.Interfaces.IServices;
using Infrastructure.MediatR.Appoinment.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InforsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InforsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserById { UserId = id };
            return await _mediator.Send(query, cancellationToken);
        }
    }
}
