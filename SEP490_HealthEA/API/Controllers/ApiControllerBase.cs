using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private IMediator _mediator = null!;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
