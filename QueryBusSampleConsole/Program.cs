using System.Threading.Tasks;
using Autofac;
using QueryBusSampleConsole.Framework.Autofac;
using QueryBusSampleConsole.Framework.Base;
using QueryBusSampleConsole.QueryHandler;
using QueryBusSampleConsole.QueryModel;

namespace QueryBusSampleConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ContainerBuilder();

            //------------------------------ Framework --------------------------
            builder.RegisterType<AutofacQueryBus>().As<IQueryBus>().InstancePerLifetimeScope();


            builder.RegisterAssemblyTypes(typeof(GetPersonDetailsQuery).Assembly).AsClosedTypesOf(typeof(IQuery<>)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(typeof(GetPersonDetailsQueryHandler).Assembly).AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces();

            var container = builder.Build();

            var bus = container.Resolve<IQueryBus>();
            var query = await bus.DispatchAsync(new GetPersonDetailsQuery() { Id = 10 });


            var result = $"Id==> {query.Id}\nName==> {query.Name}";


            Display.Fun(result);
        }

    }
}
