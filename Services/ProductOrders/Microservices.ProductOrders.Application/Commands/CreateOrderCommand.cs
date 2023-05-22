using MediatR;
using Microservices.ProductOrders.Application.Dtos;
using Microservices.SharedLibrary.Dtos;
using System.Collections.Generic;

namespace Microservices.ProductOrders.Application.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedProductOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<ProductOrderItemDto> ProductOrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
