using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoLists;
using FluentResults;

namespace FastCleanArchitecture.Application.TodoLists.Commands.DeleteTodoList;

public record DeleteTodoListCommand(Guid id) : ICommand;

internal sealed class DeleteTodoListCommandHandler : ICommandHandler<DeleteTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoListCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _todoListRepository.GetByIdAsync(request.id, cancellationToken);
        if (entity is null)
            return Result.Ok();

        _todoListRepository.Remove(entity);
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
