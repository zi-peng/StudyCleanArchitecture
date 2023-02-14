using Study.CleanArchitecture.Domain.Common;
using Study.CleanArchitecture.Domain.Enums;
using Study.CleanArchitecture.Domain.Events;

namespace Study.CleanArchitecture.Domain.Entities.TodoAggregate;

public class TodoItem : BaseAuditableEntity
{
    
    public TodoItem(int listId, string title, bool done)
    {
        ListId = listId;
        Title = title;
        Done = done;
    }

    private bool _done;

    public int ListId { get; set; }

    public string? Title { get; set; }

    public string? Note { get; set; }

    public PriorityLevel Priority { get; set; }

    public DateTime? Reminder { get; set; }

    public bool Done
    {
        get => _done;
        set
        {
            if (value && _done == false) AddDomainEvent(new TodoItemCompletedEvent(this));

            _done = value;
        }
    }

    public TodoList List { get; set; } = null!;
}