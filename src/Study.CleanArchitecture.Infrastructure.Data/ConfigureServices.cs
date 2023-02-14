using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Infrastructure.Data.Persistence;
using Study.CleanArchitecture.Infrastructure.Data.Persistence.Interceptors;
using Study.CleanArchitecture.Infrastructure.Data.Repository;

namespace Study.CleanArchitecture.Infrastructure.Data;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        
        //注入数据库
        services.AddDbContext<ApplicationDbContext>(
            options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    o => { o.EnableRetryOnFailure(); });
            });
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialise>();

        services.AddRepositories();
        return services;
    }

    /// <summary>
    /// 仓储注入
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        var assembly = typeof(TodoListRepository).Assembly;
        //FromAssemblyOf获取类的程序集
        services.Scan(x => x.FromAssemblies(assembly)
            //1.模糊匹配类名
            //.AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
            //2.按照命名空间进行注入
            .AddClasses(c => c.InExactNamespaceOf<TodoListRepository>())
            .UsingRegistrationStrategy(RegistrationStrategy.Throw)
            .AsMatchingInterface()
            .WithTransientLifetime());
        return services;
    }
}