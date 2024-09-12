using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoLists;
using FluentResults;

namespace FastCleanArchitecture.Application.TodoLists.Commands.Purge;

public record PurgeTodoListsCommand : ICommand;

internal sealed class PurgeTodoListsCommandHandler : ICommandHandler<PurgeTodoListsCommand>
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PurgeTodoListsCommandHandler(ITodoListRepository todoListRepository, IUnitOfWork unitOfWork)
    {
        _todoListRepository = todoListRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(PurgeTodoListsCommand request, CancellationToken cancellationToken)
    {
        _todoListRepository.RemoveRange(await _todoListRepository.GetAllAsync(cancellationToken));

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
