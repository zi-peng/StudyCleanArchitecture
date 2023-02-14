using FluentAssertions;
using NUnit.Framework;
using Study.CleanArchitecture.Application.Common.Exceptions;
using Study.CleanArchitecture.Application.Services.TodoItems.Commands.CreateTodoItem;
using Study.CleanArchitecture.Application.Services.TodoLists.Commands.CreateTodoList;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate;

namespace Application.IntegrationTests.TodoLists.Command;

using static Testing;

public class CreateTodoListTests : BaseTestFixture
{
    /// <summary>
    /// 必填最少字段，未填写应该抛出ValidationException
    /// </summary>
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoListCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateTodoItem()
    {
        var userId = "测试ID";

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var command = new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "Tasks"
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Title.Should().Be(command.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}