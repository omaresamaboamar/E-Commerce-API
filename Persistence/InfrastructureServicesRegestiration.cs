using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using Services;
using ServicesAbstractions;
using Microsoft.Extensions.Configuration;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Contracts;
using Persistence.Repository;
using StackExchange.Redis;
using Persistence.IdentityUser;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public static class InfrastructureServicesRegestiration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection Services ,IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(option => {
                var ConnectionString = Configuration.GetConnectionString("DefaultConnection");
                option.UseSqlServer(ConnectionString);
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<ICacheRepository, CacheRepository>();
            Services.AddSingleton<IConnectionMultiplexer , ConnectionMultiplexer>((_)=>
            {
                return ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnection"));
            });
            Services.AddDbContext<StoreIdentityDbContext>(option => {
                var ConnectionString = Configuration.GetConnectionString("IdentityConnection");
                option.UseSqlServer(ConnectionString);
            });
            Services.AddIdentityCore<ApplicationUser>(Option => 
            {

            }).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();
                
            return Services;
        }
    }
}
