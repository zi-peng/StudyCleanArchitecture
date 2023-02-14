using Study.CleanArchitecture.Application.Common.Mappings;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate;

namespace Study.CleanArchitecture.Application.Services.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}