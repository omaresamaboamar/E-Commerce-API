using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.BasketModule;
using Shared.DataTransferDto.BasketModulesDto;

namespace Services.MappingProfiles
{
    public class BasketProfile :Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<BasketItems, BasketItemsDto>().ReverseMap();
        }
    }
}
