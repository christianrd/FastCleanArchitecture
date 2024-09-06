using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems.Enums;

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

    public Guid ListId { get; private set; }

    public string? Title { get; private set; }

    public string? Note { get; private set; }

    public PriorityLevel Priority { get; private set; }

    public DateTime? Reminder { get; private set; }

    public static TodoItem Create(Guid listId, string? title, string? note, PriorityLevel priority, DateTime? reminder, string? createdBy)
    {
        return new TodoItem(Guid.NewGuid(), listId, title, note, priority, reminder, createdBy);
    }
}