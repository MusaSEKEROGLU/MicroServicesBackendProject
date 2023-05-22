using MediatR;
using Microservices.ProductOrders.Application.Commands;
using Microservices.ProductOrders.Application.Queries;
using Microservices.SharedLibrary.ControllerBases;
using Microservices.SharedLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservices.ProductOrders.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductOrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ISharedIdentityService _sharedIdentityService;
        public ProductOrdersController(IMediator mediator, ISharedIdentityService sharedIdentityService)
        {
            _mediator = mediator;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _sharedIdentityService.GetUserId});
            return CreatedAtActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);
            return CreatedAtActionResultInstance(response);
        }
    }
}
