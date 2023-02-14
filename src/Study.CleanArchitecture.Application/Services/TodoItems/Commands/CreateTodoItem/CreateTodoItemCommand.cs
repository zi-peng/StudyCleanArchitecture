using MediatR;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Domain.Entities.TodoAggregate;

namespace Study.CleanArchitecture.Application.Services.TodoItems.Commands.CreateTodoItem;

public record CreateTodoItemCommand : IRequest<int>
{
    public int ListId { get; init; }

    public string? Title { get; init; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem(request.ListId,
            request.Title, false);


        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}