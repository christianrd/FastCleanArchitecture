using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FastCleanArchitecture.Infrastructure.Data;

internal static class InitiliserExtensions
{
    public static async Task InitialiseDatabaseAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

internal sealed class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;

    private readonly ApplicationDbContext _context;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!_context.Set<TodoList>().Any())
        {
            var todolistEntity = TodoList.Create("Todo List");
            var todoItemEntities = new List<TodoItem>() {
                    TodoItem.Create(todolistEntity.Id, "Make a todo list 📃"),
                    TodoItem.Create(todolistEntity.Id, "Check off the first item ✅"),
                    TodoItem.Create(todolistEntity.Id, "Realise you've already done two things on the list! 🤯"),
                    TodoItem.Create(todolistEntity.Id, "Reward yourself with a nice, long nap 🏆")
                };
            _context.Set<TodoList>().Add(todolistEntity);
            _context.Set<TodoItem>().AddRange(todoItemEntities);

            await _context.SaveChangesAsync();
        }
    }
}