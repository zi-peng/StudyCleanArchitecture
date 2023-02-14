using Microsoft.EntityFrameworkCore;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate;

namespace Study.CleanArchitecture.Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}