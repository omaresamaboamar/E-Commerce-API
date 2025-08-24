using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferDto.IdentityDto;

namespace Shared.DataTransferDto.OrderDtos
{
    public class OrderTOReturnDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; } 
        public DateTimeOffset OrderDate { get; set; }
        public string DeliveryMethods { get; set; } = default!;
        public AddressesDto Address { get; set; } = default!;
        public string OrderStatus { get; set; } = default!;
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItemDto> Items { get; set; } = [];
    }
}
