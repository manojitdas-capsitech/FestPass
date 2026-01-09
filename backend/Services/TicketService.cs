using backend.Data;
using backend.DTOs;
using backend.Models.Entities;
using backend.Models;
using MongoDB.Driver;

namespace backend.Services
{
    public class TicketService
    {
        private readonly MongoContext _context;

        public TicketService(MongoContext context)
        {
            _context = context;
        }

        public async Task<Ticket> AssignTicketAsync(AssignTicketDto dto)
        {

            // Validate fest exists
            var festExists = await _context.Fests
                .Find(f => f.Id == dto.FestId && f.IsActive)
                .AnyAsync();

            if (!festExists)
                throw new Exception("Invalid or inactive fest");

            // Generate unique ticket code
            var ticketCode = GenerateTicketCode(dto.TicketType);

            var ticket = new Ticket
            {
                FestId = dto.FestId,
                TicketCode = ticketCode,
                UserEmail = dto.UserEmail.ToLower(),
                TicketType = dto.TicketType,
                Status = TicketStatus.Active,
                EntryUsedForSession = false,
                ConsumedMealSlotIds = new List<string>(),
                SeatNumber = dto.SeatNumber
            };

            await _context.Tickets.InsertOneAsync(ticket);

            return ticket;
        }

        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets.Find(_ => true).ToListAsync();
        }

        public async Task<List<Ticket>> GetTicketsByUserEmailAsync(string userEmail)
        {
            return await _context.Tickets
                .Find(t => string.Equals(t.UserEmail, userEmail, StringComparison.OrdinalIgnoreCase))
                .ToListAsync();

        }

        public async Task<bool> BlockTicketAsync(string ticketCode)
        {
            var update = Builders<Ticket>.Update
                .Set(t => t.Status, TicketStatus.Blocked);

            var result = await _context.Tickets.UpdateOneAsync(
                t => t.TicketCode == ticketCode,
                update
            );

            return result.ModifiedCount > 0;
        }

        // ---------- HELPERS ----------

        private string GenerateTicketCode(TicketType type)
        {
            var prefix = type.ToString().Substring(0, 3).ToUpper();
            return $"{prefix}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
    }

}
