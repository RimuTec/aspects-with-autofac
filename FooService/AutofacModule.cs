using Autofac;
using Autofac.Extras.DynamicProxy;
using FooService.Controllers;
using FooService.DataAccess;
using FooService.DataAccess.Repositories;
using FooService.Services;
using Microsoft.Extensions.Configuration;

namespace FooService
{
   public class AutofacModule : Module
   {
      public AutofacModule(IConfiguration configuration)
      {
         _configuration = configuration;
      }

      protected override void Load(ContainerBuilder containerBuilder)
      {
         // Register repositories
         containerBuilder
            .RegisterGeneric(typeof(RepositoryBase<>))
            .As(typeof(IRepository<>));
         // Registering open generic types:
         // https://autofaccn.readthedocs.io/en/latest/register/registration.html#open-generic-components

         // Register controllers
         containerBuilder
            .RegisterType<WeatherForecastController>()
            .EnableClassInterceptors();

         // Register services
         containerBuilder
            .RegisterType<ObservationService>()
            .As<IObservationService>()
            .EnableInterfaceInterceptors()
            .InterceptedBy(typeof(TransactionAspect));

         // Others
         containerBuilder
            .RegisterType<SessionAccessor>()
            .As<ISessionAccessor>();
         containerBuilder
            .RegisterType<TransactionAspect>();
         containerBuilder
            .RegisterType<Seeder>()
            .EnableClassInterceptors()
            .InterceptedBy(typeof(TransactionAspect));

         var database = new Database(_configuration);
         containerBuilder.RegisterInstance(database.CreateSessionFactory());
      }

      private readonly IConfiguration _configuration;
   }
}
