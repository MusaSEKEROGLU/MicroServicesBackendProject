using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Microservices.Beverages.Api.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
