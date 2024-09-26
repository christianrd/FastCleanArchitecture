using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists.ValueObjects;

namespace FastCleanArchitecture.Domain.TodoLists;

public sealed class TodoList : BaseAuditableEntity
{
    private TodoList(Guid id, string? title, Colour? colour, IList<TodoItem> items) : base(id)
    {
        Title = title;
        Colour = colour ?? Colour.Grey;
        Items = items;
    }

    private TodoList()
    { }

    public string? Title { get; private set; }
    public Colour Colour { get; private set; } = Colour.Grey;
    public IList<TodoItem> Items { get; private set; } = [];

    public static TodoList Create(
        string? title,
        Colour? colour = null,
        List<TodoItem>? todoItems = null)
    {
        return new TodoList(Guid.NewGuid(), title, colour, todoItems ?? new List<TodoItem>());
    }

    public static TodoList UpdateTitle(string title, TodoList todoList)
    {
        todoList.Title = title;
        todoList.ModifiedAtUtc = DateTime.UtcNow;
        return todoList;
    }
}
