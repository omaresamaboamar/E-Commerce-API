using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.OrderModule;
using Domain.Models.ProductModule;
using Services.Specification;
using ServicesAbstractions;
using Shared.DataTransferDto.IdentityDto;
using Shared.DataTransferDto.OrderDtos;

namespace Services
{
    public class OrderService(IMapper _mapper,IUnitOfWork _unitOfWork ,IBasketRepository basketRepository) : IOrderServices
    {
        public async Task<OrderTOReturnDto> CreateOrderAsync(OrderDto orderDto, string email)
        {
            //MAp Address 
            var OrderAddress = _mapper.Map<AddressesDto, OrderAddress>(orderDto.Address);

            // Get Basket Items
            var Basket = await basketRepository.GetBasketAsync(orderDto.BasketId)
                ?? throw new BasketNotFoundException(orderDto.BasketId);

            // create Order Items list 

            List<OrderItem> orderItems = [];

            var ProductRepo = _unitOfWork.GetRepository<Product, int>();

            foreach (var item in Basket.Items)  
            {
                var Product = await ProductRepo.GetByIdAsync (item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                var orderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductID = Product.Id,
                        ProductName = Product.Name,
                        PictureUrl = Product.PictureUrl
                    },
                    Price = Product.Price,
                    Quantity = item.Quantity
                };
                orderItems.Add(orderItem);
            }

            // Delivery Method

            var DeliveryMethod =await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId)
                                    ?? throw new DeliveryMethodNotFoundException(orderDto.DeliveryMethodId);

            var SupTotal = orderItems.Sum(I => I.Quantity *I.Price);

            var Order = new Order(email, OrderAddress,  DeliveryMethod, orderItems, SupTotal);
            await _unitOfWork.GetRepository<Order, Guid>().AddAsync(Order);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<Order, OrderTOReturnDto>(Order);
        }

        public async Task<IEnumerable<OrderTOReturnDto>> GetAllOrderAsync(string email)
        {
            var spec = new OrderWithSpecifications(email);
            var order =await _unitOfWork.GetRepository<Order ,Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderTOReturnDto>>(order);
        }

        public async Task<IEnumerable<DeliveryMethodsDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods =await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodsDto>>(DeliveryMethods);
        }

        public async Task<OrderTOReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderWithSpecifications(id);
            var order =await _unitOfWork.GetRepository<Order ,Guid>().GetByIdAsync(spec);
            return _mapper.Map<Order ,OrderTOReturnDto>(order);
        }
    }
}
