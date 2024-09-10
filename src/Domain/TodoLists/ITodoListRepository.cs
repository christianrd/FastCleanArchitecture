namespace FastCleanArchitecture.Domain.TodoLists;

public interface ITodoListRepository
{
    /// <summary>
    /// Get a todo list by title.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TodoList?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a todo list.
    /// </summary>
    /// <param name="todoList"><see cref="TodoList"/></param>
    void Add(TodoList todoList);

    /// <summary>
    /// Remove a todo list.
    /// </summary>
    /// <param name="todoList"><see cref="TodoList"/></param>
    void Remove(TodoList todoList);

    /// <summary>
    /// Remove a range of todo list.
    /// </summary>
    /// <param name="todoLists"><see cref="TodoList"/></param>
    void RemoveRange(List<TodoList> todoLists);

    /// <summary>
    /// Update a todo list.
    /// </summary>
    /// <param name="todoList"><see cref="TodoList"/></param>
    void Update(TodoList todoList);
}
