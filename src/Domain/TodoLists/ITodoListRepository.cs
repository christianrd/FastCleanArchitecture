namespace FastCleanArchitecture.Domain.TodoLists;

public interface ITodoListRepository
{
    Task<TodoList> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    void Add(TodoList list);
}