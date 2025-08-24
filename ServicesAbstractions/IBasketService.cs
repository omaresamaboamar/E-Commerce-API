using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferDto.BasketModulesDto;

namespace ServicesAbstractions
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(string Key);
        Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketAsync(string Key);
    }
}
