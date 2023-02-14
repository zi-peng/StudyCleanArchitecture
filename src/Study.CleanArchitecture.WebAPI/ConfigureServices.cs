using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Study.CleanArchitecture.Application.Interfaces;
using Study.CleanArchitecture.Infrastructure.Data.Persistence;
using Study.CleanArchitecture.WebAPI.Const;
using Study.CleanArchitecture.WebAPI.Filters;
using Study.CleanArchitecture.WebAPI.Services;

namespace Study.CleanArchitecture.WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        //添加身份认证
        services.AddAuthentication("Bearer").AddCookie()
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false
                };
            });

        //添加授权
        services.AddAuthorization(c =>
        {
            c.AddPolicy(ApiScopes.CleanArchitectureWebApi, p => p.RequireClaim("scope", "api1"));
        });

        //健康检查
        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        services.AddControllersWithViews(options =>
            options.Filters.Add<ApiExceptionFilterAttribute>());

        //auto validation
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(ApiScopes.CleanArchitectureWebApi,
                new OpenApiInfo { Title = "Protected API", Version = "v1" });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Authorization format : Bearer {token}",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                BearerFormat = "JWT"
            });
            //添加安全要求
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            options.OperationFilter<AuthorizeCheckOperationFilter>();
        });

        return services;
    }
}