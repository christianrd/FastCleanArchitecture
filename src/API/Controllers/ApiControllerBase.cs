namespace FastCleanArchitecture.API.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly ISender Sender;

    public ApiControllerBase(ISender Sender) => this.Sender = Sender;
}
