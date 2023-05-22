using Microservices.Beverages.Api.Dtos;
using Microservices.SharedLibrary.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Beverages.Api.Services.Abstract
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAllAsync();

        Task<Response<CategoryDto>> CreateAsync(CategoryDto category);

        Task<Response<CategoryDto>> GetByIdAsync(string id);
    }
}
