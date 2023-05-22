using Dapper;
using Microservices.SharedLibrary.Dtos;
using Microservices.ShoppingDiscounts.Api.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.ShoppingDiscounts.Api.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;

            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var deleteStatus = await _dbConnection.ExecuteAsync("Delete from discount Where id = @Id", new { Id = id });
            if (deleteStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Discount>("Select * from discount");
            return Response<List<Discount>>.Success(discounts.ToList(), 200);

        }

        public async Task<Response<Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discounts = await _dbConnection.QueryAsync<Discount>
                ("Select * from discount Where userid = @UserId and code = @Code", new { UserId = userId, Code = code });
            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount == null)
            {
                return Response<Discount>.Fail("Discout not found", 404);
            }
            return Response<Discount>.Success(hasDiscount,200);

        }

        public async Task<Response<Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Discount>
                ("Select * from discount Where id = @Id", new { Id = id })).SingleOrDefault();
            if (discount == null)
            {
                return Response<Discount>.Fail("Discount not found", 404);
            }
            return Response<Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync
                ("INSERT INTO discount(userid,rate,code)VALUES(@UserId,@Rate,@Code)", discount);
            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Bir hata meydana geldi", 500);
        }

        public async Task<Response<NoContent>> Update(Discount discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync
                ("Update discount Set userid = @UserId,rate = @Rate,code =@Code Where id = @Id", discount);
            if (updateStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Discount not found", 404);
        }
    }
}
