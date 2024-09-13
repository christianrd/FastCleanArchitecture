using System.Reflection;
using FastCleanArchitecture.Application.Common.Messaging;
using FastCleanArchitecture.Domain.Common;
using FastCleanArchitecture.Infrastructure.Data;

namespace FastCleanArchitecture.ArchitectureTests;

internal abstract class BaseTest
{
    protected static readonly Assembly DomainAssembly = typeof(BaseEntity).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IBaseCommand).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(ApplicationDbContext).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(Program).Assembly;
}
