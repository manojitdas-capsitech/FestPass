
using MongoDB.Driver;
using backend.Models.Entities;

namespace backend.Data
{

    public static class MongoIndexes
    {
        public static void CreateIndexes(MongoContext context)
        {
            // TicketCode must be unique
            var ticketIndex = Builders<Ticket>.IndexKeys.Ascending(t => t.TicketCode);
            context.Tickets.Indexes.CreateOne(
                new CreateIndexModel<Ticket>(
                    ticketIndex,
                    new CreateIndexOptions { Unique = true }
                )
            );

            // Fast lookup for scan logs
            var scanLogIndex = Builders<ScanLog>.IndexKeys
                .Ascending(s => s.TicketCode)
                .Descending(s => s.ScannedAt);

            context.ScanLogs.Indexes.CreateOne(
                new CreateIndexModel<ScanLog>(scanLogIndex)
            );

            // Prevent duplicate meal consumption per slot
            var mealIndex = Builders<MealConsumption>.IndexKeys
                .Ascending(m => m.TicketCode)
                .Ascending(m => m.MealSlotId);

            context.MealConsumptions.Indexes.CreateOne(
                new CreateIndexModel<MealConsumption>(
                    mealIndex,
                    new CreateIndexOptions { Unique = true }
                )
            );
        }
    }
}
