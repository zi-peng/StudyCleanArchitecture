using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Study.CleanArchitecture.WebAPI.Const;

namespace Study.CleanArchitecture.WebAPI.Controllers;

[ApiExplorerSettings(GroupName = ApiScopes.CleanArchitectureWebApi)]
[Authorize(ApiScopes.CleanArchitectureWebApi)]
[ApiController]
[Route("api/[controller]")]
public class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}