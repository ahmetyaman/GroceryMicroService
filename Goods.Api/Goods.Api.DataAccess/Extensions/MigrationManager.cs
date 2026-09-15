using Goods.Api.Goods.Api.DataAccess.Concrete.Context;
using Microsoft.EntityFrameworkCore;

namespace Goods.Api.Goods.Api.DataAccess.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    GoodsDbContext dbContext = scope.ServiceProvider.GetRequiredService<GoodsDbContext>();

                    if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                    {
                        dbContext.Database.MigrateAsync().Wait();
                    }

                    GoodsDbContextSeed.SeedAsync(dbContext).Wait();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return host;
        }
    }
}