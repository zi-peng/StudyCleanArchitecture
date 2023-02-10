using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Domain.Entities;
using Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot;
using Study.CleanArchitecture.Infrastructure.Data.Common;
using Study.CleanArchitecture.Infrastructure.Data.Persistence.Interceptors;

namespace Study.CleanArchitecture.Infrastructure.Data.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;
    private readonly IMediator _mediator;

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options,
        IMediator mediator,
        AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
        : base(options)
    {
        _mediator = mediator;
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    public DbSet<TodoList?> TodoLists => Set<TodoList>();

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}