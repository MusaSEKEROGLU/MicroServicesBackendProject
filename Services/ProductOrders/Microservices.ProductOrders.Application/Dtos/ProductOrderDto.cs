using Microservices.ProductOrders.Domain.ProductOrdersAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.ProductOrders.Application.Dtos
{
    public class ProductOrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public AddressDto Address { get; set; }
        public string BuyerId { get; set; }
        public List<ProductOrderItemDto> ProductOrderItems { get; set; }
    }
}
