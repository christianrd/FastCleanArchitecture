using FastCleanArchitecture.Application.Common.Interfaces;

namespace FastCleanArchitecture.Application.FeatureName.Queries.CleanArchitectureUseCase;

public record FastCleanArchitectureUseCaseQuery : IQuery<object>
{
}

internal sealed class FastCleanArchitectureUseCaseQueryHandler : IQueryHandler<FastCleanArchitectureUseCaseQuery, object>
{
    public async Task<object> Handle(FastCleanArchitectureUseCaseQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}