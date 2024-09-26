using FastCleanArchitecture.API.Infrastructure;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;
using FluentResults;

namespace FastCleanArchitecture.API.Endpoints;

public class TodoLists : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetTodoLists);
    }

    private async Task<Result<TodosVm>> GetTodoLists(ISender sender)
        => await sender.Send(new GetTodosQuery());
}
