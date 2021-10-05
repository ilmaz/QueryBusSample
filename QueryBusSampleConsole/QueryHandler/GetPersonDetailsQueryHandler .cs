using System.Threading;
using System.Threading.Tasks;
using QueryBusSampleConsole.Framework.Base;
using QueryBusSampleConsole.QueryModel;

namespace QueryBusSampleConsole.QueryHandler
{
    public class GetPersonDetailsQueryHandler :
        IQueryHandler<GetPersonDetailsQuery, PersonQueryResult>
    {

        public Task<PersonQueryResult> HandleAsync(GetPersonDetailsQuery query, CancellationToken cancellationToken)
        {
            return Task.FromResult(new PersonQueryResult { Id = query.Id, Name = "Ilmaz" });
        }
    }
}