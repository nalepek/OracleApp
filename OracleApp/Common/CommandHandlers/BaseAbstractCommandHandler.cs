using System.Threading.Tasks;
using MediatR;

namespace OracleApp.Common.CommandHandlers
{
    public abstract class BaseAbstractCommandHandler<TCommand, TResponse> : AsyncRequestHandler<TCommand, TResponse>, ICommand
        where TCommand : IRequest<TResponse>
    {
        protected abstract override Task<TResponse> HandleCore(TCommand request);
    }

    public abstract class BaseAbstractCommandHandler<TCommand> : AsyncRequestHandler<TCommand>, ICommand
        where TCommand : IRequest
    {
        protected abstract override Task HandleCore(TCommand request);
    }
}
