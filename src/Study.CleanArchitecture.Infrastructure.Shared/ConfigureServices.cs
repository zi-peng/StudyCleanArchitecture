using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Infrastructure.Shared.Files;
using Study.CleanArchitecture.Infrastructure.Shared.Services;

namespace Study.CleanArchitecture.Infrastructure.Shared;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureSharedServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<ICsvFileBuilder, CsvFileBuilder>();
        return services;
    }
}