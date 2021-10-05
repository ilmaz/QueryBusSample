using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;

namespace QueryBusSampleConsole.Framework.RequestHandler
{
    public abstract class RequestHandlerBase
    {
        public abstract Task<object?> Handle(object request, CancellationToken cancellationToken,
            ILifetimeScope scope);

        protected static THandler GetHandler<THandler>(ILifetimeScope scope)
        {
            THandler handler;

            try
            {
                handler = scope.Resolve<THandler>();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error constructing handler for request of type {typeof(THandler)}. Register your handlers with the container.", e);
            }

            if (handler == null)
            {
                throw new InvalidOperationException($"Handler was not found for request of type {typeof(THandler)}. Register your handlers with the container.");
            }

            return handler;
        }
    }
}