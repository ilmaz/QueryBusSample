using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using QueryBusSampleConsole.Framework.Base;

namespace QueryBusSampleConsole.Framework.Autofac.RequestHandler
{
    public class RequestHandlerWrapperImpl<TRequest, TResponse> : RequestHandlerWrapper<TResponse>
        where TRequest : IQuery<TResponse>
    {
        public override async Task<object?> Handle(object request, CancellationToken cancellationToken,
            ILifetimeScope scope) =>
            await Handle((IQuery<TResponse>)request, cancellationToken, scope).ConfigureAwait(false);

        public override Task<TResponse> Handle(IQuery<TResponse> request, CancellationToken cancellationToken,
            ILifetimeScope scope)
        {
            Task<TResponse> Handler() => GetHandler<IQueryHandler<TRequest, TResponse>>(scope).HandleAsync((TRequest)request, cancellationToken);

            return scope
                .Resolve<IEnumerable<IPipelineBehavior<TRequest, TResponse>>>()
                .Reverse()
                .Aggregate((RequestHandlerDelegate<TResponse>)Handler, (next, pipeline) => () => pipeline.Handle((TRequest)request, next))();
        }
    }
}