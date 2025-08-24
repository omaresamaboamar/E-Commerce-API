using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.OrderModule;
using Shared.DataTransferDto.IdentityDto;
using Shared.DataTransferDto.OrderDtos;

namespace Services.MappingProfiles
{
    internal class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressesDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderTOReturnDto>()
                     .ForMember(D => D.DeliveryMethods, O => O.MapFrom(S => S.DeliveryMethod.ShortName));  
            
            CreateMap<OrderItem, OrderItemDto>().ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                                                .ForMember(D => D.PictureUrl, O => O.MapFrom(S => S.Product.PictureUrl));

            CreateMap<DeliveryMethod , DeliveryMethodsDto>().ReverseMap();
        }
    }
}
