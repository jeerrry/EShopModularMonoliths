using MediatR;

namespace Shared.CQRS;

public interface ICommand : ICommand<uint>
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}