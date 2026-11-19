using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.IdentityUser;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbcontext,UserManager<ApplicationUser> _userManager ,
        RoleManager<IdentityRole> _roleManager ,StoreIdentityDbContext _identityDbContext) : IDataSeeding
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


                if (!_dbcontext.Set<DeliveryMethod>().Any())
                {
                    var DeliveryMethodsData = File.ReadAllText(@"..\Persistence\Data\DataSeed\delivery.json");
                    var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                    if (DeliveryMethods is not null && DeliveryMethods.Any())
                    {
                        _dbcontext.Set<DeliveryMethod>().AddRange(DeliveryMethods);
                    }
                }

                _dbcontext.SaveChanges();

            }
            catch (Exception ex)
            {

                
            }
        }

        public async void IdentityDataSeed()
        {
            if (!_roleManager.Roles.Any())
            {
               await _roleManager.CreateAsync(new IdentityRole("Admin"));
               await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            
            if (!_userManager.Users.Any())
            {
                try
                {
                    var user01 = new ApplicationUser()
                    {
                        Email = "MohamedAhmed@gmail.com",
                        DisplayName = "Mohamed Ahmed",
                        PhoneNumber = "01002745855",
                        UserName = "MohamedAhmed",
                    };
                    var user02 = new ApplicationUser()
                    {
                        Email = "SalmaAhmed@gmail.com",
                        DisplayName = "Salma Ahmed",
                        PhoneNumber = "01002476845",
                        UserName = "SalmaAhmed",
                    };

                    await _userManager.CreateAsync(user01, "Mohamed@12345");
                    await _userManager.CreateAsync(user02, "Salma@12345");

                    await _userManager.AddToRoleAsync(user01, "Admin");
                    await _userManager.AddToRoleAsync(user02, "SuperAdmin");
                    
                    await _identityDbContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
