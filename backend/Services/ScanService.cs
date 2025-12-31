
using MongoDB.Driver;
using backend.Data;
using backend.Models.Entities;
using backend.Models;
using backend.DTOs;

namespace backend.Services
{

    public class ScanService
    {
        private readonly MongoContext _context;

        public ScanService(MongoContext context)
        {
            _context = context;
        }

        // ENTRY SCAN
        public async Task<ScanResponseDto> HandleEntryScanAsync(string ticketCode)
        {
            var ticket = await _context.Tickets
                .Find(t => t.TicketCode == ticketCode && t.Status == TicketStatus.Active)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                return Fail("Invalid or inactive ticket");
            }

            if (ticket.EntryUsedForSession)
            {
                return Fail("Entry already used for this session");
            }

            // Mark entry used
            var update = Builders<Ticket>.Update
                .Set(t => t.EntryUsedForSession, true);

            await _context.Tickets.UpdateOneAsync(
                t => t.TicketCode == ticketCode,
                update
            );

            // Log scan
            await LogScan(ticketCode, ScanType.Entry);

            return Success("Entry allowed");
        }

        // FOOD SCAN
        public async Task<ScanResponseDto> HandleFoodScanAsync(string ticketCode)
        {
            var ticket = await _context.Tickets
                .Find(t => t.TicketCode == ticketCode && t.Status == TicketStatus.Active)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                return Fail("Invalid or inactive ticket");
            }

            if (!ticket.EntryUsedForSession)
            {
                return Fail("Entry not recorded for current session");
            }

            // Detect active meal slot by server time
            var now = DateTime.UtcNow.TimeOfDay;

            var mealSlot = await _context.MealSlots.Find(slot =>
                now >= slot.StartTime &&
                now <= slot.EndTime
            ).FirstOrDefaultAsync();

            if (mealSlot == null)
            {
                return Fail("Food service not available at this time");
            }

            if (ticket.ConsumedMealSlotIds.Contains(mealSlot.Id))
            {
                return Fail("Meal already consumed for this slot");
            }

            // Add consumed meal slot
            var update = Builders<Ticket>.Update
                .AddToSet(t => t.ConsumedMealSlotIds, mealSlot.Id);

            await _context.Tickets.UpdateOneAsync(
                t => t.TicketCode == ticketCode,
                update
            );

            // Log meal consumption
            var mealConsumption = new MealConsumption
            {
                TicketCode = ticketCode,
                MealSlotId = mealSlot.Id,
                ConsumedAt = DateTime.UtcNow
            };

            await _context.MealConsumptions.InsertOneAsync(mealConsumption);

            // Log scan
            await LogScan(ticketCode, ScanType.Food);

            return Success($"Food allowed: {mealSlot.MealType}");
        }

        // -------- HELPERS --------

        private async Task LogScan(string ticketCode, ScanType scanType)
        {
            var log = new ScanLog
            {
                TicketCode = ticketCode,
                ScanType = scanType,
                ScannedAt = DateTime.UtcNow,
                ScannedBy = "SYSTEM"
            };

            await _context.ScanLogs.InsertOneAsync(log);
        }

        private ScanResponseDto Success(string message)
        {
            return new ScanResponseDto
            {
                IsSuccess = true,
                Message = message
            };
        }

        private ScanResponseDto Fail(string message)
        {
            return new ScanResponseDto
            {
                IsSuccess = false,
                Message = message
            };
        }
    }
}

