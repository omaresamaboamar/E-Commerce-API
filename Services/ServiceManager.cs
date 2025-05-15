using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using ServicesAbstractions;

namespace Services
{
    public class ServiceManager(IUnitOfWork _unitOfWork, IMapper _mapper) : IServiceManager
    {
        private readonly Lazy<IProductService> _LazeproductService =
            new Lazy<IProductService>(() => new ProductServices(_unitOfWork, _mapper));
        public IProductService productService => _LazeproductService.Value;
    }
}
