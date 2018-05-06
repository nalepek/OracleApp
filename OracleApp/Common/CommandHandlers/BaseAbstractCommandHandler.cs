using System.Threading.Tasks;
using MediatR;

namespace OracleApp.Common.CommandHandlers
{
    public abstract class BaseAbstractCommandHandler<TCommand, TResponse> : AsyncRequestHandler<TCommand, TResponse>, ICommand
        where TCommand : IRequest<TResponse>
    {
        protected abstract override Task<TResponse> HandleCore(TCommand request);
    }
}
