using Study.CleanArchitecture.Domain.Common;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate;

namespace Study.CleanArchitecture.Domain.Events;

public class TodoItemDeletedEvent : BaseEvent
{
    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }

    public TodoItem Item { get; }
}