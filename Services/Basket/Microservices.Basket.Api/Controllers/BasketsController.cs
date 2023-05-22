using Microservices.Basket.Api.Dtos;
using Microservices.Basket.Api.Services;
using Microservices.SharedLibrary.ControllerBases;
using Microservices.SharedLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservices.Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            //var claims =HttpContext.User.Claims;  //breakPoint için Get
            return CreatedAtActionResultInstance(await _basketService.GetBasket(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            var response = await _basketService.SaveOrUpdate(basketDto);
            return CreatedAtActionResultInstance(response);
        }


        [HttpDelete]
        public async Task<IActionResult> SaveOrUpdateBasket()
        {
            return CreatedAtActionResultInstance(await _basketService.Delete(_sharedIdentityService.GetUserId));
        }
    }
}
