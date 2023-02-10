using Study.CleanArchitecture.Application.Common.Mappings;
using Study.CleanArchitecture.Domain.Entities;
using Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot;

namespace Study.CleanArchitecture.Application.Common.Models;

// Note: This is currently just used to demonstrate applying multiple IMapFrom attributes.
public class LookupDto : IMapFrom<TodoList>, IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public string? Title { get; set; }
}
