using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Study.CleanArchitecture.Infrastructure.Data.Persistence;

public class ApplicationDbContextInitialise
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialise> _logger;


    public ApplicationDbContextInitialise(ILogger<ApplicationDbContextInitialise> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsMySql()) await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}