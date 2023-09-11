using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class DataSeed
    {
        public static async Task SeedAsyncTask(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HeaderValidated = null,
                    MissingFieldFound = null
                };


                string path = AppContext.BaseDirectory;


                //Check if there's any data currently on the db
                if (!context.Brands.Any())
                {
                    using (StreamReader streamReader = new StreamReader(path + @"/Csvs/brands.csv"))
                    {
                        using (CsvReader csvReader = new CsvReader(streamReader, config))
                        {
                            List<Brand> brands = csvReader.GetRecords<Brand>().ToList();
                            
                            await context.Brands.AddRangeAsync(brands);
                            await context.SaveChangesAsync();
                        }
                    }

                }

                if (!context.Categories.Any())
                {
                    using (StreamReader categoriesReader = new StreamReader(path + @"/Csvs/categories.csv"))
                    {
                        using (CsvReader csvCategories = new CsvReader(categoriesReader, config))
                        {
                            List<Category> categories = csvCategories.GetRecords<Category>().ToList();

                            await context.Categories.AddRangeAsync(categories);
                            await context.SaveChangesAsync();
                        }
                    }
                }
                if (!context.Products.Any())
                {
                    using (StreamReader productsReader = new StreamReader(path + @"/Csvs/products.csv"))
                    {
                        using (CsvReader csvProducts = new CsvReader(productsReader, config))
                        {
                            List<Product> productsCsvList = csvProducts.GetRecords<Product>().ToList();

                            List<Product> products = new List<Product>();
                            foreach (Product item in productsCsvList)
                            {
                                products.Add(new Product()
                                {
                                    Id = item.Id,
                                    BrandId = item.BrandId,
                                    Name = item.Name,
                                    CategoryId = item.CategoryId,
                                    CreatedAt = item.CreatedAt,
                                    Price = item.Price
                                });
                            }
                            

                            await context.Products.AddRangeAsync(products);
                            await context.SaveChangesAsync();
                        }
                    }
                }

                if (!context.Roles.Any())
                {
                    List < Role > roles = new List<Role>()
                    {
                        new Role { Name = "Admin"},
                        new Role { Name ="Manager"},
                        new Role { Name ="Employee"}
                    };

                    await context.Roles.AddRangeAsync(roles);
                    await context.SaveChangesAsync();
                }
            }


            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<DataSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
