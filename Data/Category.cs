using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    public class Category : MongoEntity
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
    }
}
