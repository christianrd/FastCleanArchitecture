using MediatR;

namespace FastCleanArchitecture.Application.Common.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}