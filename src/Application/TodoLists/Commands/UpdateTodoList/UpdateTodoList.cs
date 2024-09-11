using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoLists;
using FluentResults;

namespace FastCleanArchitecture.Application.TodoLists.Commands.UpdateTodoList;

public record UpdateTodoListCommand : ICommand
{
    public Guid Id { get; init; }
    public string? Title { get; init; }
}

internal sealed class UpdateTodoListCommandHandler : ICommandHandler<UpdateTodoListCommand>
{
    private readonly ITodoListRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoListCommandHandler(ITodoListRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (entity is null)
            return Result.Fail("Todo list not found.");

        _repository.Update(TodoList.UpdateTitle(request.Title!, entity));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
