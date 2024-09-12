using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;

public sealed record UpdateTodoItemCommand : ICommand
{
    [FromRoute]
    public Guid Id { get; init; }

    [FromBody]
    public BodyItemRequest Body { get; set; } = new BodyItemRequest();

    public record BodyItemRequest
    {
        public string? Title { get; init; }
        public bool Done { get; init; }
    }
}

internal sealed class UpdateTodoItemCommandHandler : ICommandHandler<UpdateTodoItemCommand>
{
    private readonly ITodoItemRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoItemCommandHandler(ITodoItemRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (item is null) return Result.Ok();

        _repository.Update(TodoItem.Update(request.Body.Title, request.Body.Done, item));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
