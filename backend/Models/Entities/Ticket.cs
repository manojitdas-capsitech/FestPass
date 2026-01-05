using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace backend.Models.Entities
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
        public string FestId { get; set; } = null!;
        public string TicketCode { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public TicketType TicketType { get; set; }
        public TicketStatus Status { get; set; }

        public bool EntryUsedForSession { get; set; }
        public List<string> ConsumedMealSlotIds { get; set; } = new();

        public string? SeatNumber { get; set; } // VIP/VVIP
    }

}
