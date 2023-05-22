using Microservices.Beverages.Api.Dtos;
using Microservices.Beverages.Api.Services.Abstract;
using Microservices.SharedLibrary.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Microservices.Beverages.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetAllAsync();

            return CreatedAtActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _categoryService.GetByIdAsync(id);

            return CreatedAtActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto categoryDto)
        {
            var response = await _categoryService.CreateAsync(categoryDto);

            return CreatedAtActionResultInstance(response);
        }
    }
}
