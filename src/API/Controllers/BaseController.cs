using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastCleanArchitecture.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected readonly ISender Sender;

    public BaseController(ISender Sender) => this.Sender = Sender;
}