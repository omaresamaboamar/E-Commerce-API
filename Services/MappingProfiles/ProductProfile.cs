using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.ProductModule;
using Shared.DataTransferDto.ProductModulesDto;

namespace Services.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dist => dist.ProductBrand,
                    options => options.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dist => dist.ProductType,
                    options => options.MapFrom(src => src.ProductType.Name));
            CreateMap<ProductBrand, BrandsDto>();
            CreateMap<ProductType , TypeDto>();
        }
    }
}
