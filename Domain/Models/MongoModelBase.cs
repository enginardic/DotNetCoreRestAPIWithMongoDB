using Newtonsoft.Json;

namespace Domain.Models
{
    public class MongoModelBase
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
