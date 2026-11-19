using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstractions;

namespace Services
{
    public static class ApplicationServicesRegestiration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            Services.AddScoped<IServiceManager, ServiceManagerWithFactory>();
            Services.AddScoped<IProductService, ProductServices>();
            Services.AddScoped<Func<IProductService>>(
                provider => () => provider.GetRequiredService<IProductService>());

            Services.AddScoped<IBasketService, BasketServices>();
            Services.AddScoped<Func<IBasketService>>(
                provider => () => provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
            Services.AddScoped<Func<IAuthenticationServices>>(
                provider => () => provider.GetRequiredService<IAuthenticationServices>());

            Services.AddScoped<IOrderServices, OrderService>();
            Services.AddScoped<Func<IOrderServices>>(
                provider => () => provider.GetRequiredService<IOrderServices>());

            Services.AddScoped<ICacheServices, CacheServices>();
            return Services;
        }
    }
}
