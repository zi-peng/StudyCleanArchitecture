using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using NUnit.Framework;
using Respawn;
using Respawn.Graph;
using Study.CleanArchitecture.Infrastructure.Data.Persistence;

namespace Application.IntegrationTests;

[SetUpFixture]
public class Testing
{
    private static WebApplicationFactory<Program> _factory = null!;
    private static IConfiguration _configuration = null!;
    private static IServiceScopeFactory _scopeFactory = null!;
    private static string? _currentUserId;

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _factory = new CustomWebApplicationFactory();
        _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        _configuration = _factory.Services.GetRequiredService<IConfiguration>();
    }

    public static string? GetCurrentUserId()
    {
        return _currentUserId;
    }

    /// <summary>
    /// mediator初始化
    /// </summary>
    /// <param name="request"></param>
    /// <typeparam name="TResponse"></typeparam>
    /// <returns></returns>
    public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        using var scope = _scopeFactory.CreateScope();

        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        return await mediator.Send(request);
    }

    /// <summary>
    /// 重置数据库设置
    /// </summary>
    public static async Task ResetState()
    {
        await using var conn = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        await conn.OpenAsync();
        var respawn = await Respawner.CreateAsync(conn,
            new RespawnerOptions
            {
                TablesToIgnore = new Table[] { "__EFMigrationsHistory" },
                DbAdapter = DbAdapter.MySql
            });

        await respawn.ResetAsync(conn);
    }

    public static async Task<TEntity?> FindAsync<TEntity>(params object[] keyValues)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.FindAsync<TEntity>(keyValues);
    }

    public static async Task AddAsync<TEntity>(TEntity entity)
        where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Add(entity);

        await context.SaveChangesAsync();
    }

    public static async Task<int> CountAsync<TEntity>() where TEntity : class
    {
        using var scope = _scopeFactory.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return await context.Set<TEntity>().CountAsync();
    }
}