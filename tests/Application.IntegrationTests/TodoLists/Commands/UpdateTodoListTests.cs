using FastCleanArchitecture.Application.Common.Exceptions;
using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Application.TodoLists.Commands.Update;
using FastCleanArchitecture.Domain.TodoLists;

namespace Application.IntegrationTests.TodoLists.Commands;

internal sealed class UpdateTodoListTests : BaseIntegrationTest
{
    [Test]
    public async Task UpdateTodoList_ShouldReturnAFailure_WhenTodoListNotFound()
    {
        // Arrange
        var command = new UpdateTodoListCommand
        {
            Id = Guid.NewGuid(),
            Body = new UpdateTodoListCommand.BodyListRequest { Title = "Title to update" }
        };

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Test]
    public async Task UpdateTodoList_ShouldThrowValidationException_WhenTitleIsNotUnique()
    {
        // Arrange
        var listId = await Sender.Send(new CreateTodoListCommand { Title = "New List" });
        var command = new UpdateTodoListCommand
        {
            Id = listId.Value,
            Body = new UpdateTodoListCommand.BodyListRequest { Title = "New List" }
        };

        // Act
        var action = () => Sender.Send(command);

        // Assert
        (await action.Should().ThrowAsync<ValidationException>()
            .Where(ex => ex.Errors.First().PropertyName.Contains("Title")))
            .And.Errors.First().ErrorMessage.Should().Contain("'Title' must be unique.");
    }

    [Test]
    public async Task UpdateTodoList_ShouldUpdateTodoList_WhenValidParam()
    {
        // Arrange
        var listId = await Sender.Send(new CreateTodoListCommand { Title = "Other List" });
        var command = new UpdateTodoListCommand
        {
            Id = listId.Value,
            Body = new UpdateTodoListCommand.BodyListRequest { Title = "Updated List" }
        };

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var item = await FindAsync<TodoList>(command.Id);
        item!.Title.Should().Be(command.Body.Title);
    }
}
