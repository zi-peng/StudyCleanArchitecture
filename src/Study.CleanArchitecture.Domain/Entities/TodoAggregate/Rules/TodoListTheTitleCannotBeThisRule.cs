using Study.CleanArchitecture.Domain.Common;

namespace Study.CleanArchitecture.Domain.Entities.TodoAggregate.Rules;

/// <summary>
/// 规约(领域模型中,判断是否满足一定的条件)
/// </summary>
public class TodoListTheTitleCannotBeThisRule : IBusinessRule
{
    private readonly string _title;

    internal TodoListTheTitleCannotBeThisRule(string title)
    {
        _title = title;
    }

    public bool IsBroken()
    {
        return _title == "123456";
    }


    public string Message => "标题不能等于123456";
}