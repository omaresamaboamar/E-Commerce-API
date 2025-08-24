using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServicesAbstractions;

namespace Services
{
    public class ServiceManagerWithFactory(Func<IProductService> productFactory ,
        Func<IOrderServices> orderFactory,
        Func<IAuthenticationServices> authenticationFactory,
        Func<IBasketService> basketFactory) : IServiceManager
    {
        public IProductService productService => productFactory.Invoke();

        public IBasketService BasketService => basketFactory.Invoke();
        public IAuthenticationServices AuthenticationServices => authenticationFactory.Invoke();

        public IOrderServices OrderService => orderFactory.Invoke();
    }
}
