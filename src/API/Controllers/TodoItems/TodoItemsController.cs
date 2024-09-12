using FastCleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using FastCleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;
using FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastCleanArchitecture.API.Controllers.TodoItems;

[Route("api/[controller]")]
public sealed class TodoItemsController(ISender sender) : ApiControllerBase(sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateTodoItem([FromBody] CreateTodoItemCommand request)
    {
        await Sender.Send(request);

        return NoContent();
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateTodoItem(UpdateTodoItemCommand request)
    {
        var result = await Sender.Send(request);
        if (result.IsFailed)
            return NotFound("Item not found.");

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteTodoItem([FromRoute] DeleteTodoItemCommand request)
    {
        await Sender.Send(request);
        return NoContent();
    }
}
