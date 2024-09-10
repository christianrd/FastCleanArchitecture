using FastCleanArchitecture.Application.TodoLists.Commands.CreatTodoList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastCleanArchitecture.API.Controllers;

[Route("api/[controller]")]
public sealed class TodoListsController(ISender Sender) : BaseController(Sender)
{
    [HttpPost]
    public async Task<IActionResult> CreateTodoList(CreateTodoListCommand request)
    {
        var result = await Sender.Send(request);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return NoContent();
    }
}