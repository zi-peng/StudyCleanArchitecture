namespace Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot;

public interface ITodoListRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TodoList?> GetByIdAsync(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    Task AddAsync(TodoList customer);
}