using MediatR;
using Microservices.ProductOrders.Application.Commands;
using Microservices.ProductOrders.Application.Dtos;
using Microservices.ProductOrders.Domain.ProductOrdersAggregate;
using Microservices.ProductOrders.Infrastructure;
using Microservices.SharedLibrary.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace Microservices.ProductOrders.Application.CommandHandlers
{
    public class CreatedOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedProductOrderDto>>
    {
        private readonly OrderDbContext _context;
        public CreatedOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<Response<CreatedProductOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAdress = new Address(request.Address.Province, request.Address.District,
                request.Address.Street, request.Address.ZipCode, request.Address.Line);

            ProductOrder newProductOrder = new ProductOrder(request.BuyerId, newAdress);


            request.ProductOrderItems.ForEach(x =>
            {
                newProductOrder.AddProductOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            await _context.ProductOrders.AddAsync(newProductOrder);
            await _context.SaveChangesAsync();

            return Response<CreatedProductOrderDto>.Success
                (new CreatedProductOrderDto { ProductOrderId = newProductOrder.Id }, 200);
        }
    }
}
