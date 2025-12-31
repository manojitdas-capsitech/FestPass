using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models.Entities
{
    public class Fest
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<FestDay> Days { get; set; } = new();
        public List<Session> Sessions { get; set; } = new();

        public bool IsActive { get; set; }
    }

}
