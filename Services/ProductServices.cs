using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServicesAbstractions;
using Shared.DataTransferDto;

namespace Services
{
    public class ProductServices(IUnitOfWork _unitOfWork , IMapper _mapper) : IProductService
    {
        public async Task<IEnumerable<BrandsDto>> GetAllBrandsAsync()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync(); 
            return _mapper.Map<IEnumerable<ProductBrand>, IEnumerable <BrandsDto>>(Brands);
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Product = await _unitOfWork.GetRepository<Product , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(Product);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTybesAsync()
        {
            var Type =await _unitOfWork.GetRepository<ProductType , int>().GetAllAsync();
            return _mapper.Map<IEnumerable<ProductType>,IEnumerable <TypeDto>>(Type);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var ProductId = await _unitOfWork.GetRepository<Product ,int>().GetByIdAsync(id);
            return _mapper.Map<Product, ProductDto>(ProductId);

        }
    }
}
