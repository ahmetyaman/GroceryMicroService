using Autofac;
using Autofac.Extensions.DependencyInjection;
using Goods.Api.Goods.Api.Business.DependencyResolver;
using Goods.Api.Goods.Api.DataAccess.Extensions;

namespace Goods.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build()
                 .MigrateDatabase()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                           .ConfigureContainer<ContainerBuilder>(builder =>
                           {
                               builder.RegisterModule(new AutofacModule());
                           })

                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())

                  .ConfigureWebHostDefaults(webBuilder =>
                  {
                      webBuilder.UseStartup<Startup>();
                  })

               .ConfigureLogging(logging =>
               {
                   logging.ClearProviders();
                   logging.SetMinimumLevel(LogLevel.Trace);
               });
        }
    }
}