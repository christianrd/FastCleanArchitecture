using FastCleanArchitecture.Application.Common.Models;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists;
using Mapster;

namespace FastCleanArchitecture.Application.Common.Mappings;

internal static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<TodoItem, TodoItemDto>.NewConfig()
            .Map(dest => dest.Priority, src => (int)src.Priority);

        TypeAdapterConfig<TodoList, LookupDto>.NewConfig();
        TypeAdapterConfig<TodoItem, LookupDto>.NewConfig();
    }
}
