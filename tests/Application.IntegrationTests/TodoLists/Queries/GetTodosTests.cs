using Application.IntegrationTests.Infrastructure;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;
using FluentAssertions;

namespace Application.IntegrationTests.TodoLists.Queries;

internal class GetTodosTests : BaseIntegrationTest
{
    private readonly GetTodosQuery _query = new();

    [Test]
    public async Task GetTodos_ShouldReturnPriorityLevels_WhenListIsNotEmpty()
    {
        // Act
        var result = await Sender.Send(_query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.PriorityLevels.Should().NotBeEmpty();
    }

    [Test]
    public async Task GetTodos_ShouldReturnAllListAndItems_WhenThereAreDataAndIsSuccessful()
    {
        // Act
        var result = await Sender.Send(_query);

        // Assert
        result.Value.Lists.Should().NotBeEmpty();
        result.Value.Lists.First().Items.Should().NotBeEmpty();
    }
}
