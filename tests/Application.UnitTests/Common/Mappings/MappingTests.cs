using FastCleanArchitecture.Application.Common.Mappings;
using FastCleanArchitecture.Application.Common.Models;
using FastCleanArchitecture.Application.TodoLists.Queries.GetTodos;
using FastCleanArchitecture.Domain.TodoItems;
using FastCleanArchitecture.Domain.TodoLists;
using FluentAssertions;
using Mapster;
using MapsterMapper;
using System.Runtime.Serialization;

namespace FastCleanArchitecture.Application.UnitTests.Common.Mappings;

[TestFixture]
internal sealed class MappingTests
{
    private readonly TypeAdapterConfig _config;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        MapsterConfig.Configure();

        _config = TypeAdapterConfig.GlobalSettings;

        _mapper = new Mapper();
    }

    [Test]
    [TestCase(typeof(TodoList), typeof(TodoListDto))]
    [TestCase(typeof(TodoItem), typeof(TodoItemDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        // TODO: Figure out an alternative approach to the now obsolete `FormatterServices.GetUninitializedObject` method.
#pragma warning disable SYSLIB0050 // Type or member is obsolete
        return FormatterServices.GetUninitializedObject(type);
#pragma warning restore SYSLIB0050 // Type or member is obsolete
    }
}
