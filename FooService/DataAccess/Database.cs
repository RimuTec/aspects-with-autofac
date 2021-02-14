using System;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace FooService.DataAccess
{
   public class Database
   {
      public Database(IConfiguration configuration)
      {
         ConnectionString = configuration["Database:ConnectionString"];
      }

      public void RunMigrations()
      {
         var serviceProvider = CreateServices();

         // Put the database update into a scope to ensure that all resources,
         // i.e., the injected dependencies, will be disposed when no longer 
         // needed.
         using (var scope = serviceProvider.CreateScope())
         {
            RunMigrations(scope.ServiceProvider);
         }
      }

      /// <summary>
      /// Configure the dependency injection services
      /// </summary>
      private IServiceProvider CreateServices()
      {
         return new ServiceCollection()
             // Add common FluentMigrator services
             .AddFluentMigratorCore()
             .ConfigureRunner(rb => rb
                 // Add SqlServer support to FluentMigrator
                 .AddSqlServer()
                 // Set the connection string
                 .WithGlobalConnectionString(ConnectionString)
                 // Define the assembly containing the migrations
                 .ScanIn(typeof(Database).Assembly).For.Migrations())
             // Enable logging to console in the FluentMigrator way
             .AddLogging(lb => lb.AddFluentMigratorConsole())
             // Build the service provider
             .BuildServiceProvider(false);
      }

      public ISessionFactory CreateSessionFactory()
      {
         RunMigrations();

         return Fluently.Configure()
             .Database(
                 MsSqlConfiguration.MsSql2012.ConnectionString(ConnectionString)
             )
             .Mappings(m => m.FluentMappings
                 .AddFromAssemblyOf<Database>()
                 .Conventions.Add(FluentNHibernate.Conventions.Helpers.ForeignKey.EndsWith("Oid")))
             .BuildSessionFactory();
      }

      /// <summary>
      /// Update the database
      /// </summary>
      private static void RunMigrations(IServiceProvider serviceProvider)
      {
         // Instantiate the runner
         var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

         // Execute the migrations
         runner.MigrateUp();
      }

      //private const string ConnectionString = "Server=database,1433;Database=mahi-cmdline;User ID=sa;Password=PassWord42";
      private readonly string ConnectionString;
   }
}
