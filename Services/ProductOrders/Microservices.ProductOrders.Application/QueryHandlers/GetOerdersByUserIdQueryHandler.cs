using MediatR;
using Microservices.ProductOrders.Application.Dtos;
using Microservices.ProductOrders.Application.Mapping;
using Microservices.ProductOrders.Application.Queries;
using Microservices.ProductOrders.Infrastructure;
using Microservices.SharedLibrary.Dtos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.ProductOrders.Application.QueryHandlers
{
    public class GetOerdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<ProductOrderDto>>>
    {
        private readonly OrderDbContext _context;
        public GetOerdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<ProductOrderDto>>> Handle(GetOrdersByUserIdQuery request,
            CancellationToken cancellationToken)
        {
            //boş ise
            var orders = await _context.ProductOrders.Include
                (x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync();
            
            if (!orders.Any()) 
            {
                return Response<List<ProductOrderDto>>.Success(new List<ProductOrderDto>(),200);
            }
            //dolu ise
            var orderDto = ObjectMapper.Mapper.Map<List<ProductOrderDto>>(orders);
            return Response<List<ProductOrderDto>>.Success(orderDto, 200);
        }
    }
}
