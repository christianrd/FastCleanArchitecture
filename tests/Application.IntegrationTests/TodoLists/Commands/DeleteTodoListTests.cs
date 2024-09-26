using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Application.TodoLists.Commands.Delete;
using FastCleanArchitecture.Domain.TodoLists;

namespace Application.IntegrationTests.TodoLists.Commands;

internal class DeleteTodoListTests : BaseIntegrationTest
{
    [Test]
    public async Task DeleteTodoList_ShouldDeleteTodoList_WhenRequestIsSuccessful()
    {
        var listId = await Sender.Send(new CreateTodoListCommand { Title = "New list" });

        // Act
        await Sender.Send(new DeleteTodoListCommand(listId.Value));

        // Assert
        var list = await FindAsync<TodoList>(listId.Value);
        list.Should().BeNull();
    }
}
