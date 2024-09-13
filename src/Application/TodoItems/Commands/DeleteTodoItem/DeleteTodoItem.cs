using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;
using FluentResults;
using MediatR;

namespace FastCleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;

public record DeleteTodoItemCommand(Guid Id) : ICommand;

internal sealed class DeleteTodoItemCommandHandler : ICommandHandler<DeleteTodoItemCommand>
{
    private readonly ITodoItemRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTodoItemCommandHandler(ITodoItemRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (entity is null)
            return Result.Ok();

        _repository.Remove(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
