namespace Web.ApiGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)



           .ConfigureAppConfiguration((hostingContex, config) =>
            {
                config.SetBasePath(hostingContex.HostingEnvironment.ContentRootPath)
                .AddJsonFile("Configurations/ocelot.json").AddEnvironmentVariables();
            })

           .ConfigureWebHostDefaults(webBuilder =>
           {
               webBuilder.UseStartup<Startup>();
           });
        }
    }
}