using Goods.Api.Goods.Api.Entities.Concrete;

namespace Goods.Api.Goods.Api.DataAccess.Concrete.Context
{
    public class GoodsDbContextSeed
    {
        public static async Task SeedAsync(GoodsDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(GetPreconfiguredCategories());
                await context.SaveChangesAsync();
            }

            if (!context.Products.Any())
            {
                context.Products.AddRange(GetPreconfiguredProducts());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product {  Name = "iPhone 13", Description = "Apple'ın en yeni akıllı telefonu.", Price = 999.99m, CategoryId = 1, CreatedDate = DateTime.Now },
                new Product {  Name = "Samsung Galaxy S21", Description = "Samsung'un amiral gemisi akıllı telefonu.", Price = 899.99m, CategoryId = 1, CreatedDate = DateTime.Now },
                new Product {  Name = "Nike Air Max", Description = "Rahat ve şık spor ayakkabı.", Price = 149.99m, CategoryId = 4, CreatedDate = DateTime.Now },
                new Product {  Name = "Levi's Kot Pantolon", Description = "Klasik ve dayanıklı kot pantolon.", Price = 79.99m, CategoryId = 2, CreatedDate = DateTime.Now },
                new Product {  Name = "Moleskine Defter", Description = "Kaliteli ve şık bir defter.", Price = 19.99m, CategoryId = 5, CreatedDate = DateTime.Now }
            };
        }

        private static IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>
            {
                new Category {  Name = "Teknoloji", Description = "Akıllı telefonlar, bilgisayarlar ve elektronik ürünler.", CreatedDate = DateTime.Now },
                new Category {  Name = "Moda & Giyim", Description = "Kadın, erkek ve çocuk giyim ürünleri ile aksesuarlar.", CreatedDate = DateTime.Now },
                new Category {  Name = "Ev & Yaşam", Description = "Mobilya, dekorasyon ürünleri ve mutfak gereçleri.", CreatedDate = DateTime.Now },
                new Category {  Name = "Spor & Outdoor", Description = "Spor ekipmanları, kamp malzemeleri ve spor kıyafetleri.", CreatedDate = DateTime.Now },
                new Category {  Name = "Kitap & Hobi", Description = "Romanlar, kişisel gelişim kitapları ve sanatsal hobiler.", CreatedDate = DateTime.Now }
            };
        }
    }
}