using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Infrastructure.Data.Persistence;

namespace Application.IntegrationTests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });
        builder.ConfigureServices((builder, services) =>
        {
            services
                .Remove<ICurrentUserService>()
                .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                    //TODO 测试ID暂时写死
                    s.UserId == "测试ID"));

            services
                .Remove<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>(
                    options =>
                    {
                        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")),
                            o => { o.EnableRetryOnFailure(); });
                    });
        });
    }
}