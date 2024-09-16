using Application.IntegrationTests.Infrastructure;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;
using FluentAssertions;

namespace Application.IntegrationTests.TodoLists.Queries.GetTodos;

internal class GetTodosTests : BaseIntegrationTest
{
    [Test]
    public async Task GetTodos_ShouldReturnPriorityLevels_WhenListIsNotEmpty()
    {
        // Arrange
        var query = new GetTodosQuery();

        // Act
        var result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.ValueOrDefault.PriorityLevels.Should().NotBeEmpty();
    }
}
