using FastCleanArchitecture.Domain.TodoItems.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FastCleanArchitecture.Application.TodoItems.EventHandlers;

internal sealed class TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    : INotificationHandler<TodoItemCreatedEvent>
{
    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("FastCleanAchitecture Domain Event: {DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
    }
}
