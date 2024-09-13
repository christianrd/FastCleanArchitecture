using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists;
using FastCleanArchitecture.Infrastructure.Data;
using FastCleanArchitecture.Infrastructure.Data.Repositories;
using FastCleanArchitecture.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FastCleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
#if (UseOracle)
            options.UseOracle(connectionString);
#else
            options.UseSqlServer(connectionString);
#endif
        });

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<ITodoListRepository, TodoListRepository>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    public static async Task UseInfrastructureAsync(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            await app.InitialiseDatabaseAsync();

        app.UseCustomExceptionHandler();
    }

    internal static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
