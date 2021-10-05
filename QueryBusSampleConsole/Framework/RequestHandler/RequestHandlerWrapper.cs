using System.Threading;
using System.Threading.Tasks;
using Autofac;
using QueryBusSampleConsole.Framework.Base;

namespace QueryBusSampleConsole.Framework.RequestHandler
{
    public abstract class RequestHandlerWrapper<TResponse> : RequestHandlerBase
    {
        public abstract Task<TResponse> Handle(IQuery<TResponse> request, CancellationToken cancellationToken,
            ILifetimeScope scope);
    }
}