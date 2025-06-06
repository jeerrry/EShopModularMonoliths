using MediatR;

namespace Shared.Contracts.CQRS;

public interface ICommandHandler<in TCommand> : ICommandHandler<TCommand, uint>
    where TCommand : ICommand<uint>
{
}

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : notnull
{
}