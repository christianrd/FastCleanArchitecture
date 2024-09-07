using FastCleanArchitecture.Domain.TodoLists;
using FluentValidation;

namespace FastCleanArchitecture.Application.TodoLists.Commands.CreatTodoList;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    private readonly ITodoListRepository _todoListRepository;

    public CreateTodoListCommandValidator(ITodoListRepository todoListRepository)
    {
        _todoListRepository = todoListRepository;

        RuleFor(v => v.Title)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        var entity = await _todoListRepository.GetByTitleAsync(title, cancellationToken);

        return entity is not null;
    }
}