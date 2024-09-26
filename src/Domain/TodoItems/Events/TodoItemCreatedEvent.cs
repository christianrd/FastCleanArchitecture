namespace FastCleanArchitecture.Domain.TodoItems.Events;

public sealed record TodoItemCreatedEvent(TodoItem Item) : IDomainEvent;
