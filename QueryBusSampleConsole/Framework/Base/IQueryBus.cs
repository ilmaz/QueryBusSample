using System.Threading;
using System.Threading.Tasks;

namespace QueryBusSampleConsole.Framework.Base
{
    public interface IQueryBus
    {
        Task<TResponse> DispatchAsync<TResponse>(IQuery<TResponse> request,
            CancellationToken cancellationToken = default);
    }
}