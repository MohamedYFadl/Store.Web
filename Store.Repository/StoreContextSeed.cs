using Microsoft.Extensions.Logging;
using Store.Data.Contexts;
using Store.Data.Entities;
using System.Text.Json;

namespace Store.Repository
{
    public class StoreContextSeed 
    {
        public static async Task SeedAsync(StoreDbContext Context ,ILoggerFactory loggerFactory) {

            try
            {
                if (Context.ProductBrands != null && !Context.ProductBrands.Any()) {
                    var BrandData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                    if(Brands is not null)
                        await Context.ProductBrands.AddRangeAsync(Brands);                
                }
                if (Context.ProductTypes != null && !Context.ProductTypes.Any())
                {
                    var TypesData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var Types = JsonSerializer.Deserialize<List<ProductType>>(TypesData);
                    if (Types is not null)
                        await Context.ProductTypes.AddRangeAsync(Types);
                }
                if (Context.Products != null && !Context.Products.Any())
                {
                    var ProductsData = File.ReadAllText("../Store.Repository/SeedData/brands.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products is not null)
                        await Context.Products.AddRangeAsync(Products);
                }

                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex.Message);
            }
        
        }
    }
}
