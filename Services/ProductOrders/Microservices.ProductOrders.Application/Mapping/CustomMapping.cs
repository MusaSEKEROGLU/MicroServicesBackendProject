using AutoMapper;
using Microservices.ProductOrders.Application.Dtos;
using Microservices.ProductOrders.Domain.ProductOrdersAggregate;


namespace Microservices.ProductOrders.Application.Mapping
{
    public class CustomMapping :Profile
    {
        public CustomMapping()
        {
            CreateMap<ProductOrder, ProductOrderDto>().ReverseMap();
            CreateMap<ProductOrderItem, ProductOrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
