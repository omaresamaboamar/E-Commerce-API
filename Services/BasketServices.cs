using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.BasketModule;
using ServicesAbstractions;
using Shared.DataTransferDto.BasketModulesDto;

namespace Services
{
    public class BasketServices(IBasketRepository basketRepository , IMapper _mapper) : IBasketService
    {
        public async Task<CustomerBasketDto> CreateOrUpdateBasketAsync(CustomerBasketDto basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            var CreateOrUpdateBasket = await basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (CreateOrUpdateBasket != null)
            {
                return await GetBasketAsync(basket.Id);
            }
            else
                throw new Exception("Can Not Create OR Update Basket");
        }

        public async Task<bool> DeleteBasketAsync(string Key)
        {
           return await basketRepository.DeleteBasketAsync(Key);
        }

        public async Task<CustomerBasketDto> GetBasketAsync(string Key)
        {
            var Basket = await basketRepository.GetBasketAsync(Key);
            if(Basket != null)
               return _mapper.Map<CustomerBasket , CustomerBasketDto >(Basket);
            else 
                throw new BasketNotFoundException(Key);
        }
    }
}
