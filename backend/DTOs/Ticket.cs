using backend.Models;

namespace backend.DTOs
{
    public class AssignTicketDto
    {
        public TicketType TicketType { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string? SeatNumber { get; set; }
    }

}