using FastCleanArchitecture.Application.Common.Messaging;

namespace FastCleanArchitecture.Application.FeatureName.Commands.FastCleanArchitectureUseCase;

public record FastCleanArchitectureUseCaseCommand : ICommand<object>
{
}

internal sealed class FastCleanArchitectureUseCaseCommandHandler : ICommandHandler<FastCleanArchitectureUseCaseCommand, object>
{
    public async Task<Result<object>> Handle(FastCleanArchitectureUseCaseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
