using Microservices.SharedLibrary.ControllerBases;
using Microservices.SharedLibrary.Services;
using Microservices.ShoppingDiscounts.Api.Models;
using Microservices.ShoppingDiscounts.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservices.ShoppingDiscounts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingDiscountsController : CustomBaseController
    {
        private readonly ISharedIdentityService _identityService;
        private readonly IDiscountService _discountService;
        public ShoppingDiscountsController(ISharedIdentityService identityService, IDiscountService discountService)
        {
            _identityService = identityService;
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreatedAtActionResultInstance(await _discountService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAll(int id)
        {
            var discount = await _discountService.GetById(id);
            return CreatedAtActionResultInstance(discount);
        }

        [HttpGet]
        [Route("api/[controller]/[action]/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _identityService.GetUserId;
            var discount = await _discountService.GetByCodeAndUserId(userId, code); 
            return CreatedAtActionResultInstance(discount);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Discount discount)
        {
            return CreatedAtActionResultInstance(await _discountService.Save(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Discount discount)
        {
            return CreatedAtActionResultInstance(await _discountService.Update(discount));
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreatedAtActionResultInstance(await _discountService.Delete(id));
        }
    }
}
