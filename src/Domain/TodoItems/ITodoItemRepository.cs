namespace FastCleanArchitecture.Domain.TodoItems;

public interface ITodoItemRepository
{
    /// <summary>
    /// Get Item by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TodoItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Add item to the list.
    /// </summary>
    /// <param name="todoItem"><see cref="TodoItem"/></param>
    void Add(TodoItem todoItem);

    /// <summary>
    /// Delete a item.
    /// </summary>
    /// <param name="todoItem"><see cref="TodoItem"/></param>
    void Remove(TodoItem todoItem);

    /// <summary>
    /// Update a item.
    /// </summary>
    /// <param name="todoItem"><see cref="TodoItem"/></param>
    void Update(TodoItem todoItem);
}
