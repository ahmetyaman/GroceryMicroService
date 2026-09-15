using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions
{
    public static class ConfiguratioInfraExtensions
    {
        public static void AddConfiguratioInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<IConfiguration>(configuration);
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }
    }
}