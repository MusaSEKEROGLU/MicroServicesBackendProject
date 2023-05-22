using Microservices.Beverages.Api.Dtos;
using Microservices.Beverages.Api.Services.Abstract;
using Microservices.SharedLibrary.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservices.Beverages.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeveragesController : CustomBaseController
    {
        private readonly IBeveragesService _beveragesService;
        public BeveragesController(IBeveragesService beveragesService)
        {
            _beveragesService = beveragesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _beveragesService.GetAllAsync();

            return CreatedAtActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _beveragesService.GetByIdAsync(id);

            return CreatedAtActionResultInstance(response);
        }

        [HttpGet]
        [Route("/api/[controller]/GetAllByUserId/{userId}")]
        public async Task<IActionResult> GetAllByUserId(string userId)
        {
            var response = await _beveragesService.GetAllByUserIdAsync(userId);

            return CreatedAtActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BeveragesCreateDto beveragesCreateDto)
        {
            var response = await _beveragesService.CreateAsync(beveragesCreateDto);

            return CreatedAtActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(BeveragesUpdateDto beveragesUpdateDto)
        {
            var response = await _beveragesService.UpdateAsync(beveragesUpdateDto);

            return CreatedAtActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _beveragesService.DeleteAsync(id);

            return CreatedAtActionResultInstance(response);
        }
    }
}
