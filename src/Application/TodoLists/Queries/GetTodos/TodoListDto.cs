namespace FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;

public sealed class TodoListDto
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Colour { get; set; }

    public IReadOnlyCollection<TodoItemDto> Items { get; set; } = [];
}
