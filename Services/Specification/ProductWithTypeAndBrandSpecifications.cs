using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Models.ProductModule;
using Shared.DataTransferDto.ProductModulesDto;

namespace Services.Specification
{
    internal class ProductWithTypeAndBrandSpecifications :BaseSpecification<Product ,int>
    {
        public ProductWithTypeAndBrandSpecifications(int id):base(product => product.Id == id)
        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p=> p.ProductType);
        }

        public ProductWithTypeAndBrandSpecifications(ProductQueryParameter queryParameters) : base(ApplyCritria(queryParameters))

        {
            AddIncludes(p => p.ProductBrand);
            AddIncludes(p => p.ProductType);
            ApplySortingOptions(queryParameters);
            ApplyPaging(queryParameters.PageIndex,queryParameters.PageSize);
        }

        private static Expression<Func<Product, bool>> ApplyCritria(ProductQueryParameter queryParameters)
        {
            return product =>
                    (!queryParameters.BrandId.HasValue || product.BrandId == queryParameters.BrandId.Value) &&
                    (!queryParameters.TypeId.HasValue || product.TypeId == queryParameters.TypeId.Value) &&
                    (string.IsNullOrEmpty(queryParameters.Search) ||
                    product.Name.ToLower().Contains(queryParameters.Search.ToLower()));
        }

        private void ApplySortingOptions(ProductQueryParameter queryParameters)
        {
            switch (queryParameters.Options)
            {
                case ProductSorting.NameAsc:
                    AddOrderByAsc(p => p.Name);
                    break;
                case ProductSorting.NameDesc:
                    AddOrderByDesc(p => p.Name);
                    break;
                case ProductSorting.PriceAsc:
                    AddOrderByAsc(p => p.Price);
                    break;
                case ProductSorting.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;
                default:
                    break;
            }
        }
    }
}
