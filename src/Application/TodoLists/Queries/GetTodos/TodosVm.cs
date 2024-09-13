using FastCleanArchitecture.Application.Common.Models;

namespace FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;

public sealed class TodosVm
{
    public IReadOnlyCollection<LookupDto> PriorityLevels { get; set; } = [];
    public IReadOnlyCollection<TodoListDto> Lists { get; set; } = [];
}
