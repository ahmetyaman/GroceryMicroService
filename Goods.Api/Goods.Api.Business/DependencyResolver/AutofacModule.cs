using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.CrossCuttingConcers.Caching;
using Core.CrossCuttingConcers.Caching.Microsoft;
using Core.CrossCuttingConcers.Logging.Serilog.Loggers;
using Core.Utilities.Interceptors;
using Goods.Api.Goods.Api.Business.Abstract;
using Goods.Api.Goods.Api.Business.Concrete;
using Goods.Api.Goods.Api.DataAccess.Abstract;
using Goods.Api.Goods.Api.DataAccess.Concrete;
using System.Diagnostics;

namespace Goods.Api.Goods.Api.Business.DependencyResolver
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();

            builder.RegisterType<EfProductDal>().As<IProductDal>();
            builder.RegisterType<ProductService>().As<IProductService>();

            builder.RegisterType<MemoryCacheService>().As<ICacheService>();
            builder.RegisterType<FileLogger>();
            builder.RegisterType<Stopwatch>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterseptorSelector()
                }).SingleInstance();
        }
    }
}