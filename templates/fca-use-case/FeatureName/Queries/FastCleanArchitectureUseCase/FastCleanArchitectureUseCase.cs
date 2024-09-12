using FastCleanArchitecture.Application.Common.Messaging;

namespace FastCleanArchitecture.Application.FeatureName.Queries.FastCleanArchitectureUseCase;

public record FastCleanArchitectureUseCaseQuery : IQuery<object>
{
}

internal sealed class FastCleanArchitectureUseCaseQueryHandler : IQueryHandler<FastCleanArchitectureUseCaseQuery, object>
{
    public async Task<Result<object>> Handle(FastCleanArchitectureUseCaseQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
