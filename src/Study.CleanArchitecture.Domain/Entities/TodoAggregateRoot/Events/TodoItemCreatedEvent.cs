using Study.CleanArchitecture.Domain.Common;

namespace Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot.Events;

public class TodoListCreatedEvent : BaseEvent
{
    public TodoListCreatedEvent(TodoList list)
    {
        list = list;
    }

    public TodoList list { get; }
}
