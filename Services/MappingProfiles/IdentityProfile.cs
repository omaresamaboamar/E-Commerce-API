using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.IdentityModule;
using Shared.DataTransferDto.IdentityDto;

namespace Services.MappingProfiles
{
    internal class IdentityProfile : Profile
    {
        public IdentityProfile()
        {
            CreateMap<Address, AddressesDto>().ReverseMap();
        }
    }
}
