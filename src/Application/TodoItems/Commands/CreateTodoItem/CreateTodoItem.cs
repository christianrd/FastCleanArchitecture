using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;

namespace FastCleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : ICommand<Guid>
{
    public Guid ListId { get; set; }
    public string? Title { get; set; }
}

internal sealed class CreateTodoItemCommandCommandHandler : ICommandHandler<CreateTodoItemCommand, Guid>
{
    private readonly ITodoItemRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTodoItemCommandCommandHandler(ITodoItemRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = TodoItem.Create(request.ListId, request.Title);

        _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();

        return entity.Id;
    }
}
