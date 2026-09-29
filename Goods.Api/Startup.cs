using AutoMapper;
using Core.Extensions;
using Goods.Api.Extensions;
using Goods.Api.Goods.Api.DataAccess.Concrete.Context;
using Goods.Api.Goods.Api.Entities.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.OpenApi;

namespace Goods.Api
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
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            // services.AddCustomSwagger();



            services.AddConfiguratioInfra(Configuration);

            services.ConfigureConsul(Configuration);

            #region AutoMapper

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            }, NullLoggerFactory.Instance);
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion AutoMapper

            services.AddMemoryCache();
            services.AddCustomHttpContextAccessor();
            
            services.AddDbContext<GoodsDbContext>(options =>
                options.UseSqlServer(Configuration.GetSection("ConnectionStrings:GoodsConnection").Value,
                options=>options.MigrationsAssembly("Goods.Api")
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName,"dbo")));



            #region Swagger Dependencies

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Goods api", Version = "v1" });
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

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Goods api V1"));

         //   app.UseHttpsRedirection();
            app.UseStaticHttpContext();
            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.RegisterWithConsul(lifetime);
        }
    }
}