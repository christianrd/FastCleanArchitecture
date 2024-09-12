using FastCleanArchitecture.Domain.TodoItems;

namespace FastCleanArchitecture.Infrastructure.Data.Repositories;

internal sealed class TodoItemRepository : BaseRepository<TodoItem>, ITodoItemRepository
{
    public TodoItemRepository(ApplicationDbContext context) : base(context)
    {
    }
}
