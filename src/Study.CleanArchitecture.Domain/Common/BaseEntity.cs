using System.ComponentModel.DataAnnotations.Schema;
using Study.CleanArchitecture.Domain.Exceptions;

namespace Study.CleanArchitecture.Domain.Common;

public abstract class BaseEntity
{
    private readonly List<BaseEvent> _domainEvents = new();
    public int Id { get; set; }

    [NotMapped] public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    /// <summary>
    /// 验证规则
    /// </summary>
    /// <param name="rule"></param>
    /// <exception cref="BusinessRuleValidationException"></exception>
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new BusinessRuleValidationException(rule);
    }
}