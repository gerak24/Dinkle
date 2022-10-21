using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dinkle.Infrastructure.Database
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDatabaseManager(this IServiceCollection sc)
        {
            sc.AddScoped<MartenDatabaseManager>();
            sc.AddScoped<ISourceManager>(sp => sp.GetRequiredService<MartenDatabaseManager>());
            sc.AddScoped<ITransactionManager>(sp => sp.GetRequiredService<MartenDatabaseManager>());
            sc.AddScoped(p => p.GetRequiredService<ISourceManager>().GetServerEntities());
            sc.AddScoped(p => p.GetRequiredService<ISourceManager>().GetUserEntities(p.GetUserId()));
        }

        private static Guid GetUserId(this IServiceProvider provider) =>
            !Guid.TryParse(provider.GetRequiredService<IHttpContextAccessor>().HttpContext?.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType), out var value)
                ? throw new Exception("Cannot parse user identity for initialize database context")
                : value;
    }
}