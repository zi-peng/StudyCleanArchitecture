using System.ComponentModel.DataAnnotations.Schema;
using Study.CleanArchitecture.Domain.Common;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate.Events;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate.Rules;
using Study.CleanArchitecture.Domain.ValueObjects;

namespace Study.CleanArchitecture.Domain.Entities.TodoAggregate;

public class TodoList : BaseAuditableEntity, IAggregateRoot
{
    public TodoList(string title)
    {
        CheckRule(new TodoListTheTitleCannotBeThisRule(title));
        Title = title;
        AddDomainEvent(new TodoListCreatedEvent(this));
    }

    public TodoList()
    {
    }

    public string Title { get; set; }
    
    public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}