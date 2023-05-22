using Microservices.SharedLibrary.Dtos;
using Microservices.ShoppingDiscounts.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.ShoppingDiscounts.Api.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Discount>>> GetAll();
        Task<Response<Discount>> GetById(int id);
        Task<Response<NoContent>> Save(Discount discount);
        Task<Response<NoContent>> Update(Discount discount);
        Task<Response<NoContent>> Delete(int id);
        Task<Response<Discount>> GetByCodeAndUserId(string code, string userId);
    }
}
