using Application.IntegrationTests.Infrastructure;
using FastCleanArchitecture.Application.Common.Exceptions;
using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;
using FluentAssertions;

namespace Application.IntegrationTests.TodoLists.Commands;

internal class CreateTodoListTests : BaseIntegrationTest
{
    [Test]
    public async Task CreateTodoList_ShouldThrowValidationException_WhenRequireAMinimunFields()
    {
        // Act
        var act = () => Sender.Send(new CreateTodoListCommand());

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreatTodoList_ShouldThrowValidationException_WhenRequireUniqueTitle()
    {
        // Arrange
        var command = new CreateTodoListCommand { Title = "Homeworks" };
        await Sender.Send(command);

        // Act and Assert
        await FluentActions
            .Invoking(() => Sender
            .Send(command))
            .Should()
            .ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task CreateTodoList_ShouldCreateTodoList_WhenRequestIsValidAndSuccessful()
    {
        // Arrange
        var command = new CreateTodoListCommand { Title = "Participate in a daily meeting." };

        // Act
        var result = await Sender.Send(command);

        // Assert
        var lists = await Sender.Send(new GetTodosQuery());
        var list = lists.Value.Lists.FirstOrDefault(x => x.Id!.Equals(result.Value));

        result.Value.Should().NotBeEmpty();
        list!.Title.Should().Be(command.Title);
    }
}
