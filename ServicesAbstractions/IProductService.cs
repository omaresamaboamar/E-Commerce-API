using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferDto;

namespace ServicesAbstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProductsAsync();
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandsDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTybesAsync();
    }
}
