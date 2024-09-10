using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists.ValueObjects;

namespace FastCleanArchitecture.Domain.TodoLists;

public sealed class TodoList : BaseAuditableEntity
{
    private TodoList(Guid id, string? title, Colour? colour, IList<TodoItem>? items) : base(id)
    {
        Title = title;
        Colour = colour ?? Colour.Grey;
        Items = items ?? [];
    }

    public string? Title { get; private set; }
    public Colour Colour { get; private set; }
    public IList<TodoItem> Items { get; private set; }

    public static TodoList Create(
        string? title,
        Colour? colour = null,
        IList<TodoItem>? todoItems = null)
    {
        return new TodoList(Guid.NewGuid(), title, colour, todoItems);
    }
}
