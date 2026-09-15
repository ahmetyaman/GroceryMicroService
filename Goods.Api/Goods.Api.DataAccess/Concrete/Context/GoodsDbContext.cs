using Goods.Api.Goods.Api.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Reflection;

namespace Goods.Api.Goods.Api.DataAccess.Concrete.Context
{
    public class GoodsDbContext : DbContext
    {
        public GoodsDbContext()
        {
        }

        public GoodsDbContext(DbContextOptions<GoodsDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Core.Extensions.ConfiguratioInfraExtensions.Configuration.GetSection("ConnectionStrings:GoodsConnection").Value,
                options => options.MigrationsAssembly("Goods.Api")
                .MigrationsHistoryTable(HistoryRepository.DefaultTableName, "dbo"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Assembly assemblyConfiguration = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyConfiguration);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}