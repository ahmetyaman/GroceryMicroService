using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace Goods.Api.Extensions
{
    public static class ConsulRegistration
    {

        public static IServiceCollection ConfigureConsul(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
            {
                var address = configuration["ConsulConfig:Address"];
                consulConfig.Address = new Uri(address);
            }));

            return services;
        }


        public static IApplicationBuilder RegisterWithConsul(this IApplicationBuilder app, IHostApplicationLifetime lifetime)
        {
            var consulClient = app.ApplicationServices.GetRequiredService<IConsulClient>();

            var loggingFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();

            var logger = loggingFactory.CreateLogger<IApplicationBuilder>();

            //Get some info   about server

            var features = app.Properties["server.Features"] as FeatureCollection;
            var addresses = features.Get<IServerAddressesFeature>();
            var address = addresses.Addresses.First();

            //Register consul

            var uri = new Uri(address);
            var registration = new AgentServiceRegistration()
            {
                ID = $"Goods-{uri.Port}",
                Name = "Goods",
                Address = $"{uri.Host}",
                Port = uri.Port,
                Tags = new[] { "Goods", "Goods.Api" }
            };


            logger.LogInformation("Registering with Consul");
            consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            consulClient.Agent.ServiceRegister(registration).Wait();



            lifetime.ApplicationStopped.Register(() =>
            {
                logger.LogInformation("Deregistration from Consul");

                consulClient.Agent.ServiceDeregister(registration.ID).Wait();
            });

            return app;
        }



    }
}
