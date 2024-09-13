using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists;
using Microsoft.EntityFrameworkCore;

namespace FastCleanArchitecture.Infrastructure.Data.Repositories;

internal sealed class TodoListRepository : BaseRepository<TodoList>, ITodoListRepository
{
    public TodoListRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<List<TodoList>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<TodoList>()
            .Include(t => t.Items)
            .OrderBy(x => x.Title)
            .ToListAsync(cancellationToken);

    public async Task<TodoList?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TodoList>().FirstOrDefaultAsync(x => x.Title! == title);
    }
}
