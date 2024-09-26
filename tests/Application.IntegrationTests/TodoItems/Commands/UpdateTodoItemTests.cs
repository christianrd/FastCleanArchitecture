using FastCleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItem;
using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Domain.TodoItems;

namespace Application.IntegrationTests.TodoItems.Commands;

internal sealed class UpdateTodoItemTests : BaseIntegrationTest
{
    [Test]
    public async Task UpdateTodoItem_ShouldReturnFailure_WhenItemNotFound()
    {
        // Arrange
        var command = new UpdateTodoItemCommand
        {
            Id = Guid.NewGuid(),
            Body = new UpdateTodoItemCommand.BodyItemRequest() { Title = "New Title" }
        };

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Test]
    public async Task UpdateTodoItem_ShouldUpdate_WhenValidParams()
    {
        // Arrange
        var listId = await Sender.Send(new CreateTodoListCommand
        {
            Title = "New list",
        });

        var itemId = await Sender.Send(new CreateTodoItemCommand
        {
            ListId = listId.Value,
            Title = "New item"
        });

        var command = new UpdateTodoItemCommand
        {
            Id = itemId.Value,
            Body = new UpdateTodoItemCommand.BodyItemRequest { Title = "Updated item title" }
        };

        // Act
        await Sender.Send(command);

        // Assert
        var item = await FindAsync<TodoItem>(itemId.Value);

        item.Should().NotBeNull();
        item!.Title.Should().Be(command.Body.Title);
        item.ModifiedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
