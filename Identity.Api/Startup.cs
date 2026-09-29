using Consul;
using Identity.Api.Application.Services;
using Identity.Api.Extensions;
using Microsoft.OpenApi;

namespace Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.ConfigureConsul(Configuration);


          

            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity api", Version = "v1" });
            });

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IHostApplicationLifetime lifetime)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            //   app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity api V1"));


            app.UseRouting();
    
            app.UseStaticFiles();





            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            app.RegisterWithConsul(lifetime);
        }
    }
}