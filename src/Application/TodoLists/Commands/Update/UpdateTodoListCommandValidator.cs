using FastCleanArchitecture.Domain.TodoLists;
using FluentValidation;

namespace FastCleanArchitecture.Application.TodoLists.Commands.Update;

public class UpdateTodoListCommandValidator : AbstractValidator<UpdateTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public UpdateTodoListCommandValidator(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;

        RuleFor(v => v.Body.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique")
            .OverridePropertyName(x => x.Body.Title);
    }

    public async Task<bool> BeUniqueTitle(UpdateTodoListCommand request, string title, CancellationToken cancellationToken)
    {
        var entity = await _todoListRepository.GetByIdAsync(request.Id, cancellationToken);
        return entity is not null && entity!.Title!.Equals(title, StringComparison.OrdinalIgnoreCase);
    }
}
