using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;
using Shared.DataTransferDto.ProductModulesDto;

namespace ServicesAbstractions
{
    public interface IProductService
    {
        Task<PaginationRespons<ProductDto>> GetAllProductsAsync(ProductQueryParameter queryParameters);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandsDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTybesAsync();
    }
}
