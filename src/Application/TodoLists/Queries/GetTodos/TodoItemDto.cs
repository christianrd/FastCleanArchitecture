namespace FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;

public sealed class TodoItemDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public bool Done { get; set; }
    public int Priority { get; set; }
    public string? Note { get; set; }
}
