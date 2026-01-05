
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
        public async Task<ScanResponseDto> HandleEntryScanAsync(string ticketCode, string festId)
        {
            var ticket = await _context.Tickets
                .Find(t => t.TicketCode == ticketCode && t.Status == TicketStatus.Active)
                .FirstOrDefaultAsync();

            if (ticket == null)
            {
                return Fail("Invalid or inactive ticket");
            }

            if (ticket.FestId != festId)
                return Fail("Ticket does not belong to this fest");

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
            await LogScan(ticketCode, ScanType.Entry, festId);

            return Success("Entry allowed");
        }

        // FOOD SCAN
        public async Task<ScanResponseDto> HandleFoodScanAsync(string ticketCode, string festId)
        {
            var ticket = await _context.Tickets
                .Find(t => t.TicketCode == ticketCode && t.Status == TicketStatus.Active)
                .FirstOrDefaultAsync();

            if (ticket == null)
                return Fail("Invalid or inactive ticket");

            if (ticket.FestId != festId)
                return Fail("Ticket does not belong to this fest");

            if (!ticket.EntryUsedForSession)
                return Fail("Entry not recorded for current session");

            // 🔹 Fetch fest with meals
            var fest = await _context.Fests
                .Find(f => f.Id == festId && f.IsActive)
                .FirstOrDefaultAsync();

            if (fest == null)
                return Fail("Fest not found or inactive");

            // 🔹 Use LOCAL date & time
            var now = DateTime.Now;
            var today = now.Date;
            var currentTime = now.TimeOfDay;

            // 🔹 Find today’s active meal
            var mealSlot = fest.MealSlots.FirstOrDefault(slot =>
                slot.Date.Date == today &&
                currentTime >= slot.StartTime &&
                currentTime <= slot.EndTime
            );

            if (mealSlot == null)
                return Fail("Food service not available at this time");

            if (ticket.ConsumedMealSlotIds.Contains(mealSlot.Id))
                return Fail("Meal already consumed for this slot");

            // 🔹 Mark meal consumed
            await _context.Tickets.UpdateOneAsync(
                t => t.TicketCode == ticketCode,
                Builders<Ticket>.Update.AddToSet(
                    t => t.ConsumedMealSlotIds,
                    mealSlot.Id
                )
            );

            await _context.MealConsumptions.InsertOneAsync(new MealConsumption
            {
                TicketCode = ticketCode,
                MealSlotId = mealSlot.Id,
                ConsumedAt = DateTime.UtcNow
            });

            await LogScan(ticketCode, ScanType.Food, festId);

            return Success($"Food allowed: {mealSlot.MealType}");
        }


        // -------- HELPERS --------

        private async Task LogScan(string ticketCode, ScanType scanType, string festId)
        {
            var log = new ScanLog
            {
                TicketCode = ticketCode,
                FestId = festId,
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

