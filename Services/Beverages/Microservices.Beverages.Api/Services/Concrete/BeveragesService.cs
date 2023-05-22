using AutoMapper;
using Microservices.SharedLibrary.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microservices.Beverages.Api.Models;
using System.Linq;
using Microservices.Beverages.Api.Settings;
using Microservices.Beverages.Api.Dtos;
using Microservices.Beverages.Api.Services.Abstract;

namespace Microservices.Beverages.Api.Services.Concrete
{
    public class BeveragesService : IBeveragesService
    {
        private readonly IMongoCollection<Models.Beverages> _beveragesCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public BeveragesService(IMapper mapper, IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);

            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _beveragesCollection = database.GetCollection<Models.Beverages>(databaseSettings.BeveragesCollectionName);

            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<BeveragesDto>>> GetAllAsync()
        {
            var beverages = await _beveragesCollection.Find(beverage => true).ToListAsync();
            if (beverages.Any())
            {
                foreach (var beverage in beverages)
                {
                    beverage.Category = await _categoryCollection.Find<Category>(c => c.Id == beverage.CategoryId).FirstAsync();
                }
            }
            else
            {
                beverages = new List<Models.Beverages>();
            }

            return Response<List<BeveragesDto>>.Success(_mapper.Map<List<BeveragesDto>>(beverages), 200);
        }

        public async Task<Response<BeveragesDto>> GetByIdAsync(string id)
        {
            var beverage = await _beveragesCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (beverage == null)
            {
                return Response<BeveragesDto>.Fail("beverage not found", 404);
            }
            beverage.Category = await _categoryCollection.Find<Category>(c => c.Id == beverage.CategoryId).FirstAsync();

            return Response<BeveragesDto>.Success(_mapper.Map<BeveragesDto>(beverage), 200);
        }

        public async Task<Response<List<BeveragesDto>>> GetAllByUserIdAsync(string userId)
        {
            var beverages = await _beveragesCollection.Find<Models.Beverages>(c => c.UserId == userId).ToListAsync();
            if (beverages.Any())
            {
                foreach (var beverage in beverages)
                {
                    beverage.Category = await _categoryCollection.Find<Category>(c => c.Id == beverage.CategoryId).FirstAsync();
                }
            }
            else
            {
                beverages = new List<Models.Beverages>();
            }

            return Response<List<BeveragesDto>>.Success(_mapper.Map<List<BeveragesDto>>(beverages), 200);
        }

        public async Task<Response<BeveragesDto>> CreateAsync(BeveragesCreateDto beveragesCreateDto)
        {
            var newBeverage = _mapper.Map<Models.Beverages>(beveragesCreateDto);

            newBeverage.CreatedTime = DateTime.Now;

            await _beveragesCollection.InsertOneAsync(newBeverage);

            return Response<BeveragesDto>.Success(_mapper.Map<BeveragesDto>(newBeverage), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(BeveragesUpdateDto beveragesUpdateDto)
        {
            var updateBeverage = _mapper.Map<Models.Beverages>(beveragesUpdateDto);

            var result = await _beveragesCollection.FindOneAndReplaceAsync(c => c.Id == beveragesUpdateDto.Id, updateBeverage);

            if (result == null)
            {
                return Response<NoContent>.Fail("Beverage not found", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _beveragesCollection.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            return Response<NoContent>.Fail("Beverage not found", 404);
        }
    }
}
