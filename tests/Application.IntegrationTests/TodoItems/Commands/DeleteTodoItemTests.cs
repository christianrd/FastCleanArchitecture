using FastCleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using FastCleanArchitecture.Application.TodoItems.Commands.DeleteTodoItem;
using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Domain.TodoItems;
using FluentResults;

namespace Application.IntegrationTests.TodoItems.Commands;

internal sealed class DeleteTodoItemTests : BaseIntegrationTest
{
    private Result<Guid> _listId;

    [OneTimeSetUp]
    public async Task SetUp() => _listId = await Sender.Send(new CreateTodoListCommand
    {
        Title = "New lists"
    });

    [TestCase(true)]
    [TestCase(false)]
    public async Task DeleteTodoItem_ShouldDeleteTodoItem_WhenValidParam(bool itemExists)
    {
        // Arrange
        var itemId = itemExists ? await Sender.Send(new CreateTodoItemCommand
        {
            ListId = _listId.Value,
            Title = "New item"
        }) : Guid.NewGuid();

        // Act
        await Sender.Send(new DeleteTodoItemCommand(itemId.Value));

        // Assert
        var item = await FindAsync<TodoItem>(itemId.Value);
        item.Should().BeNull();
    }
}
