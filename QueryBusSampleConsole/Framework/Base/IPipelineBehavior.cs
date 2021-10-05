using System.Threading.Tasks;

namespace QueryBusSampleConsole.Framework.Base
{
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

    public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : notnull
    {
        Task<TResponse> Handle(TRequest request,  RequestHandlerDelegate<TResponse> next);
    }
}