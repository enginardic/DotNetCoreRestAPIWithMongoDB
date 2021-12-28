using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Data
{
    public class MongoEntity : Entity<ObjectId>
    {
        //[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public override ObjectId Id { get; set; }
    }
}
