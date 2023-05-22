using Microservices.Beverages.Api.Dtos;
using Microservices.SharedLibrary.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Beverages.Api.Services.Abstract
{
    public interface IBeveragesService
    {
        Task<Response<List<BeveragesDto>>> GetAllAsync();
        Task<Response<BeveragesDto>> GetByIdAsync(string id);
        Task<Response<List<BeveragesDto>>> GetAllByUserIdAsync(string userId);
        Task<Response<BeveragesDto>> CreateAsync(BeveragesCreateDto beveragesCreateDto);
        Task<Response<NoContent>> UpdateAsync(BeveragesUpdateDto beveragesUpdateDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
