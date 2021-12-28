using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    public class Product : MongoEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("categoryId")]
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        
        [BsonElement("price")]
        public double Price { get; set; }

        [BsonElement("currency")]
        public string Currency { get; set; }

    }
}
