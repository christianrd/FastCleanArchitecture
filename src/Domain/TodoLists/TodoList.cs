using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists.ValueObjects;

namespace FastCleanArchitecture.Domain.TodoLists;

public sealed class TodoList : BaseAuditableEntity
{
    private TodoList(Guid id, string? title) : base(id)
    {
        Title = title;
    }

    public string? Title { get; private set; }
    public Colour Colour { get; private set; } = Colour.Grey;
    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();

    public static TodoList Create(string? title)
    {
        return new TodoList(Guid.NewGuid(), title);
    }
}