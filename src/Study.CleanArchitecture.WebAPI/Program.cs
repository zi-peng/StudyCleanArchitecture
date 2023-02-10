using Study.CleanArchitecture.Application;
using Study.CleanArchitecture.Infrastructure.Data;
using Study.CleanArchitecture.Infrastructure.Data.Persistence;
using Study.CleanArchitecture.Infrastructure.Shared;
using Study.CleanArchitecture.WebAPI;
using Study.CleanArchitecture.WebAPI.Const;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddWebApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddInfrastructureSharedServices(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/{ApiScopes.CleanArchitectureWebApi}/swagger.json", "My API V1");
        options.OAuthClientId("client");
        options.OAuthClientSecret("secret");
        options.OAuthAppName("Swagger UI for ApIDemo2");
        options.OAuthScopeSeparator(" ");
        options.OAuthUsePkce();
    });
    // Initialise and seed database
    using var scope = app.Services.CreateScope();
    var initialise = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialise>();
    await initialise.InitialiseAsync();
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();