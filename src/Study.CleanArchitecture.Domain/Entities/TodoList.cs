using Study.CleanArchitecture.Domain.Common;
using Study.CleanArchitecture.Domain.ValueObjects;

namespace Study.CleanArchitecture.Domain.Entities;

public class TodoList : BaseAuditableEntity
{
    public string? Title { get; set; }

    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}
