using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FooService
{
   public class Program
   {
      public static void Main(string[] args)
      {
         CreateHostBuilder(args).Build().Run();
      }

      public static IHostBuilder CreateHostBuilder(string[] args) =>
          Host.CreateDefaultBuilder(args)
              // Plug in Autofac as the service provider, for more details see
              // https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html#asp-net-core-3-0-and-generic-hosting
              .UseServiceProviderFactory(new AutofacServiceProviderFactory())
              .ConfigureWebHostDefaults(webBuilder =>
              {
                 webBuilder.UseStartup<Startup>();
              });
   }
}
