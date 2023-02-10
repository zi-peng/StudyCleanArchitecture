using MediatR;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Domain.Entities.TodoAggregateRoot;

namespace Study.CleanArchitecture.Application.Services.TodoLists.Commands.CreateTodoList;

public record CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; init; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList(request.Title);
        
        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}