namespace FastCleanArchitecture.Domain.TodoList;

public interface ITodoListRepository
{
    Task<TodoList> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    void Add(TodoList list);
}