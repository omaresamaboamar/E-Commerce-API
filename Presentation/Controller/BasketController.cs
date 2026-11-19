using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferDto.BasketModulesDto;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasket(string key)
        {
            var Basket = await _serviceManager.BasketService.GetBasketAsync(key);
            return Ok(Basket);
        }
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdateBasket(CustomerBasketDto basket)
        {
            var CreateOrUpdate = await _serviceManager.BasketService.CreateOrUpdateBasketAsync(basket);
            return Ok(CreateOrUpdate);
        }
        [HttpDelete("{Key}")]
        public async Task<ActionResult<bool>> DeleteBasket(string Key)
        {
            var IsDeleted = await _serviceManager.BasketService.DeleteBasketAsync(Key);
               return Ok(IsDeleted);
        }
    }
}
