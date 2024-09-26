using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoLists;

namespace FastCleanArchitecture.Application.TodoLists.Commands.Create;

public record CreateTodoListCommand : ICommand<Guid>
{
    public string? Title { get; set; }
}

internal sealed class CreateTodoListCommandHandler(ITodoListRepository repository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateTodoListCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = TodoList.Create(request.Title);
        repository.Add(entity);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}