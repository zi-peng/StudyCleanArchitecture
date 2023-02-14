using Study.CleanArchitecture.Domain.Common;

namespace Study.CleanArchitecture.Domain.Entities.TodoAggregate.Events;

public class TodoListCreatedEvent : BaseEvent
{
    public TodoListCreatedEvent(TodoList list)
    {
        list = list;
    }

    public TodoList list { get; }
}