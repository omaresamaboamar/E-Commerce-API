using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.ProductModule;
using Services.Specification;
using ServicesAbstractions;
using Shared;
using Shared.DataTransferDto.ProductModulesDto;

namespace Services
{
    public class ProductServices(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandsDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync(); 
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable <BrandsDto>>(Brands);
        }

        public async Task<PaginationRespons<ProductDto>> GetAllProductsAsync(ProductQueryParameter queryParameters)
        {
            var specification = new ProductWithTypeAndBrandSpecifications(queryParameters);
            var Repo = _unitOfWork.GetRepository<Product, int>();
            var products = await Repo.GetAllAsync(specification);
            var Data = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
            var ProductCount = Data.Count();
            var CountSpec = new ProductCountSpesifications(queryParameters);
            var TotalCount = await Repo.CountAsync(CountSpec);
            return new PaginationRespons<ProductDto>(queryParameters.PageIndex, ProductCount, TotalCount, Data);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTybesAsync()
        {
            var Type =await _unitOfWork.GetRepository<ProductType , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable <TypeDto>>(Type);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithTypeAndBrandSpecifications(id);
            var ProductId = await _unitOfWork.GetRepository<Product ,int>().GetByIdAsync(specification);
            if (ProductId == null)
            {
                throw new ProductNotFoundException(id);
            }
            return _mapper.Map<Product, ProductDto>(ProductId);

        }
    }
}
