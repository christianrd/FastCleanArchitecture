using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Application.TodoLists.Commands.Delete;
using FastCleanArchitecture.Application.TodoLists.Commands.Update;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;

namespace FastCleanArchitecture.API.Controllers.TodoLists;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class TodoListsController(ISender Sender) : ApiControllerBase(Sender)
{
    [HttpGet(Name = nameof(GetTodoLists))]
    public async Task<IActionResult> GetTodoLists(CancellationToken cancellation)
    {
        var result = await Sender.Send(new GetTodosQuery(), cancellation);
        return Ok(result.ValueOrDefault);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTodoList(CreateTodoListCommand request, CancellationToken cancellation)
    {
        var result = await Sender.Send(request, cancellation);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return CreatedAtRoute(nameof(GetTodoLists), result.Value);
    }

    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateTodoList(UpdateTodoListCommand request, CancellationToken cancellation)
    {
        var result = await Sender.Send(request, cancellation);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return NoContent();
    }

    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteTodoList([FromRoute] DeleteTodoListCommand request, CancellationToken cancellation)
    {
        await Sender.Send(request, cancellation);
        return NoContent();
    }
}
