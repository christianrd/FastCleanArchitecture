using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoList.ValueObjects;

namespace FastCleanArchitecture.Domain.TodoList;

public sealed class TodoList : BaseAuditableEntity
{
    private TodoList(Guid id, string? title, Colour colour, IList<TodoItem> items, string? createdBy) : base(id)
    {
        Title = title;
        Colour = colour;
        Items = items;
        CreatedBy = CreatedBy;
    }

    public string? Title { get; private set; }
    public Colour Colour { get; private set; } = Colour.Grey;
    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();

    public static TodoList Create(string? title, Colour colour, IList<TodoItem> items, string? CreatedBy)
    {
        return new TodoList(Guid.NewGuid(), title, colour, items, CreatedBy);
    }
}