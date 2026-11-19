using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServicesAbstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper , IBasketRepository basketRepository ,UserManager<ApplicationUser> _userManager ,IConfiguration _configuration) 
    {
        private readonly Lazy<IProductService> _LazeproductService =
            new Lazy<IProductService>(() => new ProductServices(_unitOfWork, _mapper));
        public IProductService productService => _LazeproductService.Value;

        private readonly Lazy<IBasketService> _LazeBasketService = 
            new Lazy<IBasketService>(() => new BasketServices(basketRepository, _mapper));
        public IBasketService BasketService => _LazeBasketService.Value;

        private readonly Lazy<IAuthenticationServices> _LazeAuthenticationService =
            new Lazy<IAuthenticationServices>(() => new AuthenticationServices(_userManager, _configuration ,_mapper));
        public IAuthenticationServices AuthenticationServices => _LazeAuthenticationService.Value;


        private readonly Lazy<IOrderServices> _LazeOrderService =
            new Lazy<IOrderServices>(() => new OrderService(_mapper, _unitOfWork, basketRepository));
        public IOrderServices OrderService => _LazeOrderService.Value;
    }
}
