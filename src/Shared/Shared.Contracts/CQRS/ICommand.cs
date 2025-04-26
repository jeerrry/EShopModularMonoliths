using MediatR;

namespace Shared.Contracts.CQRS;

public interface ICommand : ICommand<uint>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}