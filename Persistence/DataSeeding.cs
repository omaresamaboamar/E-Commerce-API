using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbcontext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                if (_dbcontext.Database.GetAppliedMigrations().Any())
                {
                    _dbcontext.Database.Migrate();
                }
                if (!_dbcontext.ProductBrands.Any())
                {
                    var ProductBrandsData = File.ReadAllText(@"..\Persistence\Data\DataSeed\brands.json");
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandsData);
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        _dbcontext.ProductBrands.AddRange(ProductBrands);
                    }

                }

                if (!_dbcontext.ProductTypes.Any())
                {
                    var ProductTypesData = File.ReadAllText(@"..\Persistence\Data\DataSeed\types.json");
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypesData);
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        _dbcontext.ProductTypes.AddRange(ProductTypes);
                    }
                }


                if (!_dbcontext.Products.Any())
                {
                    var ProductsData = File.ReadAllText(@"..\Persistence\Data\DataSeed\products.json");
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);
                    if (Products is not null && Products.Any())
                    {
                        _dbcontext.Products.AddRange(Products);
                    }
                }

                _dbcontext.SaveChanges();

            }
            catch (Exception ex)
            {

                
            }
        }
    }
}
