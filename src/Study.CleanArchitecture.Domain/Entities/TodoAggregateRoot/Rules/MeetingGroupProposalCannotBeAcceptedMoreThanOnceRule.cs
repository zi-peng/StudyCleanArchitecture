using Study.CleanArchitecture.Domain.Common;
using Study.CleanArchitecture.Domain.Enums;

namespace Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot.Rules;

/// <summary>
/// 规则
/// </summary>
public class MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule : IBusinessRule
{
    private readonly string _title;

    internal MeetingGroupProposalCannotBeAcceptedMoreThanOnceRule(string title)
    {
        _title = title;
    }

    public bool IsBroken() => _title ==null;


    public string Message => "不等等于最高";
}