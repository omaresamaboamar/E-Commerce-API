using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferDto.OrderDtos;

namespace ServicesAbstractions
{
    public interface IOrderServices
    {
        Task<OrderTOReturnDto> CreateOrderAsync(OrderDto orderDto, string email);
        Task<IEnumerable<DeliveryMethodsDto>> GetDeliveryMethodsAsync();
        Task<IEnumerable<OrderTOReturnDto>> GetAllOrderAsync(string email);
        Task<OrderTOReturnDto> GetOrderByIdAsync(Guid id);
    }
}
