using FluentValidation;

namespace FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;

public class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
{
    public UpdateTodoItemCommandValidator()
    {
        RuleFor(v => v.Body.Title)
            .MaximumLength(200)
            .NotEmpty()
            .OverridePropertyName(x => x.Body.Title);
    }
}
