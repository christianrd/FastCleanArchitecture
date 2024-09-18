using Application.IntegrationTests.Infrastructure;
using FastCleanArchitecture.Application.Common.Exceptions;
using FastCleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Domain.TodoItems;
using FluentAssertions;

namespace Application.IntegrationTests.TodoItems.Commands;

internal sealed class CreateTodoItemTests : BaseIntegrationTest
{
    [Test]
    public async Task CreateTodoItem_ShouldThrowValidation_WhenRequireMinimumFields()
    {
        // Arrange
        var command = new CreateTodoItemCommand();

        // Act & Assert
        await FluentActions.Invoking(() =>
            Sender.Send(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreateTodoItem_ShouldCreateTodoItem_WhenRequestIsValid()
    {
        // Arrange
        var listId = await Sender.Send(new CreateTodoListCommand() { Title = "New List" });

        var command = new CreateTodoItemCommand
        {
            ListId = listId.Value,
            Title = "Tasks"
        };

        // Act
        var itemId = await Sender.Send(command);

        // Assert
        var item = await FindAsync<TodoItem>(itemId.Value);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Title.Should().Be(command.Title);
        item.CreatedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
