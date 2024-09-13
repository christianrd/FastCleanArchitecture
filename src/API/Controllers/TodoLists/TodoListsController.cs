using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Application.TodoLists.Commands.Delete;
using FastCleanArchitecture.Application.TodoLists.Commands.Update;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FastCleanArchitecture.API.Controllers.TodoLists;

[Route("api/[controller]")]
public sealed class TodoListsController(ISender Sender) : ApiControllerBase(Sender)
{
    [HttpGet(Name = nameof(GetTodoLists))]
    public async Task<IActionResult> GetTodoLists()
    {
        var result = await Sender.Send(new GetTodosQuery());
        return Ok(result.ValueOrDefault);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoList(CreateTodoListCommand request)
    {
        var result = await Sender.Send(request);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return CreatedAtRoute(nameof(GetTodoLists), result.Value);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateTodoList(UpdateTodoListCommand request)
    {
        var result = await Sender.Send(request);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteTodoList([FromRoute] DeleteTodoListCommand request)
    {
        await Sender.Send(request);
        return NoContent();
    }
}
