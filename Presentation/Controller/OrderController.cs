using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.DataTransferDto.OrderDtos;

namespace Presentation.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController(IServiceManager _serviceManager) :ControllerBase
    {
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderTOReturnDto>>CreatOrder(OrderDto orderDto)
        {
           var email = User.FindFirstValue(ClaimTypes.Email);
           var order =await _serviceManager.OrderService.CreateOrderAsync(orderDto, email);
           return Ok(order);
        }

        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodsDto>>> GetDeliveryMethods()
        {
            var deliveryMethods =await _serviceManager.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderTOReturnDto>>> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders =await _serviceManager.OrderService.GetAllOrderAsync(email);
            return Ok(orders);
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderTOReturnDto>> GetOrderById(Guid id)
        {
            var order =await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}
