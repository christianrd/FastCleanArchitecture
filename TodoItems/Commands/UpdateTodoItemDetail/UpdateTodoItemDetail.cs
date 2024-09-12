using FastCleanArchitecture.Application.Common.Messaging;

namespace FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : ICommand<object>
{
}

internal sealed class FastArchitectureUseCaseCommandHandler : ICommandHandler<UpdateTodoItemDetailCommand, object>
{
    public async Task<object> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}