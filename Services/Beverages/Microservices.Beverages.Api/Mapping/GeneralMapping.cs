using AutoMapper;
using Microservices.Beverages.Api.Dtos;
using Microservices.Beverages.Api.Models;

namespace Microservices.Beverages.Api.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Models.Beverages, BeveragesDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Feature, FeatureDto>().ReverseMap();

            CreateMap<Models.Beverages, BeveragesCreateDto>().ReverseMap();
            CreateMap<Models.Beverages, BeveragesUpdateDto>().ReverseMap();
        }
    }
}
