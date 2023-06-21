using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Course.Services.Catalog.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
