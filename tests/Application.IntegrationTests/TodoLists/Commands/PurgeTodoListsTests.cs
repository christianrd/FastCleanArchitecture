using Application.IntegrationTests.Infrastructure;
using FastCleanArchitecture.Application.TodoLists.Commands.Purge;
using FastCleanArchitecture.Domain.TodoLists;
using FluentAssertions;

namespace Application.IntegrationTests.TodoLists.Commands;

internal sealed class PurgeTodoListsTests : BaseIntegrationTest
{
    [Test]
    public async Task PurgeTodoLists_ShouldDeleteAllLists_WhenValidRequest()
    {
        // Act
        await Sender.Send(new PurgeTodoListsCommand());

        // Assert
        var count = await CountAsync<TodoList>();
        count.Should().Be(0);
    }
}
