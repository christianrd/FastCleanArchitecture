using FastCleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using FastCleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;
using FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;

namespace FastCleanArchitecture.API.Controllers.TodoItems;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class TodoItemsController(ISender sender) : ApiControllerBase(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoItemCommand request, CancellationToken cancellation)
    {
        await Sender.Send(request, cancellation);

        return NoContent();
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateTodoItem(UpdateTodoItemCommand request, CancellationToken cancellation)
    {
        var result = await Sender.Send(request, cancellation);
        if (result.IsFailed)
            return NotFound("Item not found.");

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteTodoItem([FromRoute] DeleteTodoItemCommand request, CancellationToken cancellation)
    {
        await Sender.Send(request, cancellation);
        return NoContent();
    }
}
