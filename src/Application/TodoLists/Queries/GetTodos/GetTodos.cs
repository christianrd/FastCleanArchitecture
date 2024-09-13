using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Application.Common.Models;
using FastCleanArchitecture.Domain.TodoItems.Enums;
using FastCleanArchitecture.Domain.TodoLists;
using FluentResults;
using Mapster;

namespace FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;

public record GetTodosQuery : IQuery<TodosVm>;

internal sealed class GetTodosQueryHandler(ITodoListRepository todoListRepository) : IQueryHandler<GetTodosQuery, TodosVm>
{
    public async Task<Result<TodosVm>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todoLists = await todoListRepository.GetAllAsync(cancellationToken);

        return new TodosVm
        {
            PriorityLevels = Enum.GetValues(typeof(PriorityLevel))
                .Cast<PriorityLevel>()
                .Select(priorityLevel => new LookupDto { Id = (int)priorityLevel, Title = priorityLevel.ToString() })
                .ToList(),

            Lists = todoLists.Adapt<List<TodoListDto>>()
        };
    }
}
