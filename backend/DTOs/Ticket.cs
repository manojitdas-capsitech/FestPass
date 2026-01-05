using backend.Models;
using MongoDB.Bson;

namespace backend.DTOs
{
    public class AssignTicketDto
    {
        public string FestId { get; set; } = null!;
        public TicketType TicketType { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string? SeatNumber { get; set; }
    }

}