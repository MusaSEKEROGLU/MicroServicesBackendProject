using MediatR;
using Microservices.ProductOrders.Application.Dtos;
using Microservices.SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.ProductOrders.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<ProductOrderDto>>>
    {
        public string UserId { get; set; }
    }
}
