using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Autofac;
using QueryBusSampleConsole.Framework.Base;
using QueryBusSampleConsole.Framework.RequestHandler;

namespace QueryBusSampleConsole.Framework.Autofac
{

    public class AutofacQueryBus : IQueryBus
    {

        private readonly ILifetimeScope _scope;
        private static readonly ConcurrentDictionary<Type, RequestHandlerBase> _requestHandlers = new();

        public AutofacQueryBus(ILifetimeScope scope)
        {
            _scope = scope;
        }


        public Task<TResponse> DispatchAsync<TResponse>(IQuery<TResponse> query,
            CancellationToken cancellationToken = default)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryType = query.GetType();

            var handler = (RequestHandlerWrapper<TResponse>)_requestHandlers.GetOrAdd(queryType,
                static t => (RequestHandlerBase)(Activator.CreateInstance(typeof(RequestHandlerWrapperImpl<,>).MakeGenericType(t, typeof(TResponse)))
                                                 ?? throw new InvalidOperationException($"Could not create wrapper type for {t}")));

            return handler.Handle(query, cancellationToken, _scope);
        }
    }
}