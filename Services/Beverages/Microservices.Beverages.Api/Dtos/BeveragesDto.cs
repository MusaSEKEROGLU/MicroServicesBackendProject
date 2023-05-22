using Microservices.Beverages.Api.Models;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;

namespace Microservices.Beverages.Api.Dtos
{
    public class BeveragesDto
    {       
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }     
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }   
        public DateTime CreatedTime { get; set; }
        public FeatureDto Feature { get; set; }       
        public string CategoryId { get; set; }       
        public CategoryDto Category { get; set; }
    }
}
