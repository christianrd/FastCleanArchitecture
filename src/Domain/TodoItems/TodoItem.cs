using FastCleanArchitecture.Domain.TodoItems.Enums;
using FastCleanArchitecture.Domain.TodoItems.Events;

namespace FastCleanArchitecture.Domain.TodoItems;

public sealed class TodoItem : BaseAuditableEntity
{
    private TodoItem(Guid id, Guid listId, string? title, string? note, PriorityLevel priority, DateTime? reminder, string? createdBy) : base(id)
    {
        ListId = listId;
        Title = title;
        Note = note;
        Priority = priority;
        Reminder = reminder;
        CreatedBy = createdBy;
    }

    private TodoItem()
    { }

    public Guid ListId { get; private set; }

    public string? Title { get; private set; }

    public string? Note { get; private set; }

    public PriorityLevel Priority { get; private set; }

    public DateTime? Reminder { get; private set; }

    public bool Done { get; private set; }

    public static TodoItem Create(Guid listId, string? title, PriorityLevel priority = PriorityLevel.None, string? note = null, DateTime? reminder = null, string? createdBy = null)
    {
        var item = new TodoItem(Guid.NewGuid(), listId, title, note, priority, reminder, createdBy);

        item.AddDomainEvent(new TodoItemCreatedEvent(item));

        return item;
    }

    public static TodoItem Update(string? title, bool Done, TodoItem todoItem)
    {
        todoItem.Title = title;
        todoItem.Done = Done;
        todoItem.ModifiedAtUtc = DateTime.UtcNow;
        return todoItem;
    }

    public static TodoItem UpdateDetail(Guid listId, PriorityLevel priority, string? note, TodoItem todoItem)
    {
        todoItem.ListId = listId;
        todoItem.Priority = priority;
        todoItem.Note = note;
        todoItem.ModifiedAtUtc = DateTime.UtcNow;
        return todoItem;
    }
}
