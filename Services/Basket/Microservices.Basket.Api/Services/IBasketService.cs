using Microservices.Basket.Api.Dtos;
using Microservices.SharedLibrary.Dtos;
using System.Threading.Tasks;

namespace Microservices.Basket.Api.Services
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
