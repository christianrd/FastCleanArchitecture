using Application.IntegrationTests.Infrastructure;
using FastCleanArchitecture.Application.TodoItems.Commands.CreateTodoItem;
using FastCleanArchitecture.Application.TodoItems.Commands.UpdateTodoItemDetail;
using FastCleanArchitecture.Application.TodoLists.Commands.Create;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoItems.Enums;
using FluentAssertions;

namespace Application.IntegrationTests.TodoItems.Commands;

internal sealed class UpdateTodoItemDetailTests : BaseIntegrationTest
{
    [Test]
    public async Task UpdateTodoItemDetail_ShouldReturnFailure_WhenItemNotFound()
    {
        // Arrange
        var command = new UpdateTodoItemDetailCommand
        {
            Id = Guid.NewGuid(),
            Body = new UpdateTodoItemDetailCommand.BodyItemDetailRequest { }
        };

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.IsFailed.Should().BeTrue();
    }

    [Test]
    public async Task UpdateTodoItemDetail_ShouldUpdateDetail_WhenValidParam()
    {
        // Arrange
        var listId = await Sender.Send(new CreateTodoListCommand { Title = "New list" });
        var itemId = await Sender.Send(new CreateTodoItemCommand
        {
            ListId = listId.Value,
            Title = "New Item"
        });

        var command = new UpdateTodoItemDetailCommand
        {
            Id = itemId.Value,
            Body = new UpdateTodoItemDetailCommand.BodyItemDetailRequest
            {
                ListId = listId.Value,
                Note = "A nother note...",
                Priority = PriorityLevel.High
            }
        };

        // Act
        await Sender.Send(command);

        // Assert
        var item = await FindAsync<TodoItem>(itemId.Value);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(listId.Value);
        item.Note.Should().Be(command.Body.Note);
        item.Priority.Should().Be(command.Body.Priority);
        item.ModifiedAtUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
