using FastCleanArchitecture.Application.Common.Interfaces;

namespace FastCleanArchitecture.Application.FeatureName.Commands.CleanArchitectureUseCase;

public record FastCleanArchitectureUseCaseCommand : ICommand<object>
{
}

internal sealed class FastArchitectureUseCaseCommandHandler : ICommandHandler<FastCleanArchitectureUseCaseCommand, object>
{
    public async Task<object> Handle(FastCleanArchitectureUseCaseCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}