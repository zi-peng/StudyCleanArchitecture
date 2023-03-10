using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Study.CleanArchitecture.WebAPI.Filters;

public class AuthorizeCheckOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //获取是否添加登录特性
        //策略名称映射到范围
        var requiredScopes = context.MethodInfo
            .GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>()
            .Select(attr => attr.Policy)
            .Distinct();

        if (!requiredScopes.Any()) return;
        operation.Responses.Add("401", new OpenApiResponse { Description = "未经授权" });
        operation.Responses.Add("403", new OpenApiResponse { Description = "禁止访问" });

        var oAuthScheme = new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
        };

        operation.Security = new List<OpenApiSecurityRequirement>
        {
            new()
            {
                [oAuthScheme] = requiredScopes.ToList()
            }
        };
    }
}