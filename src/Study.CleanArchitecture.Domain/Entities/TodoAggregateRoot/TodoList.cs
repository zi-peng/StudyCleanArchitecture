using Study.CleanArchitecture.Domain.Common;
using Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot.Events;
using Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot.Rules;

namespace Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot;

public class TodoList : BaseAuditableEntity, IAggregateRoot
{
    public TodoList(string title)
    {
        CheckRule(new MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule(title));
        Title = title;
        AddDomainEvent(new TodoListCreatedEvent(this));
    }

    public string? Title { get; set; }

    // [Column(TypeName= "json")]
    // public Colour Colour { get; set; } = Colour.White;

    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();
}