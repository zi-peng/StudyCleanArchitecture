using Study.CleanArchitecture.Application.Common.Mappings;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate;

namespace Study.CleanArchitecture.Application.Services.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}