using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoItems.Enums;
using Microsoft.AspNetCore.Mvc;
using static FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem.UpdateTodoItemCommand;

namespace FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;

public sealed record UpdateTodoItemDetailCommand : ICommand
{
    [FromRoute]
    public Guid Id { get; init; }

    [FromBody]
    public BodyItemDetailRequest Body { get; set; } = new BodyItemDetailRequest();

    public record BodyItemDetailRequest
    {
        public Guid ListId { get; init; }

        public PriorityLevel Priority { get; init; }

        public string? Note { get; init; }
    }
}

internal sealed class UpdateTodoItemDetailCommandHandler : ICommandHandler<UpdateTodoItemDetailCommand>
{
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTodoItemDetailCommandHandler(ITodoItemRepository todoItemRepository, IUnitOfWork unitOfWork)
    {
        _todoItemRepository = todoItemRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var item = await _todoItemRepository.GetByIdAsync(request.Id, cancellationToken);

        if (item is null) return Result.Fail("Item not found.");

        _todoItemRepository.Update(TodoItem.UpdateDetail(request.Body.ListId, request.Body.Priority, request.Body.Note, item));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
