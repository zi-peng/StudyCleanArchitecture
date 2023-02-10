using Microsoft.EntityFrameworkCore;
using Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot;
using Study.CleanArchitecture.Infrastructure.Data.Persistence;

namespace Study.CleanArchitecture.Infrastructure.Data.Repository;

public class TodoListRepository : ITodoListRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public TodoListRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<TodoList?> GetByIdAsync(int id)
    {
        return await _applicationDbContext.TodoLists.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(TodoList customer)
    {
        await _applicationDbContext.AddAsync(customer);
    }
}