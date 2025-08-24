using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstractions
{
    public interface IServiceManager
    {
        public IProductService productService  { get; }
        public IBasketService BasketService { get; }
        public IAuthenticationServices AuthenticationServices { get; }
        public IOrderServices OrderService { get; }
    }
}
