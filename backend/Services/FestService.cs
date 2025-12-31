using backend.Data;
using backend.DTOs;
using backend.Models.Entities;
using MongoDB.Driver;

namespace backend.Services
{
    public class FestService
    {
        private readonly MongoContext _context;

        public FestService(MongoContext context)
        {
            _context = context;
        }

        public async Task<Fest> CreateFestAsync(CreateFestDto dto)
        {
            var fest = new Fest
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsActive = true
            };

            await _context.Fests.InsertOneAsync(fest);
            return fest;
        }

        public async Task<bool> AddFestDayAsync(string festId, CreateFestDayDto dto)
        {
            var update = Builders<Fest>.Update.Push(f => f.Days, new FestDay
            {
                DayNumber = dto.DayNumber,
                Date = dto.Date
            });

            var result = await _context.Fests.UpdateOneAsync(
                f => f.Id == MongoDB.Bson.ObjectId.Parse(festId),
                update
            );

            return result.ModifiedCount > 0;
        }

        public async Task<bool> AddSessionAsync(string festId, CreateSessionDto dto)
        {
            var update = Builders<Fest>.Update.Push(f => f.Sessions, new Session
            {
                DayNumber = dto.DayNumber,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            });

            var result = await _context.Fests.UpdateOneAsync(
                f => f.Id == MongoDB.Bson.ObjectId.Parse(festId),
                update
            );

            return result.ModifiedCount > 0;
        }

        public async Task<bool> AddMealSlotAsync(CreateMealSlotDto dto)
        {
            var slot = new MealSlot
            {
                DayNumber = dto.DayNumber,
                MealType = dto.MealType,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime
            };

            await _context.MealSlots.InsertOneAsync(slot);
            return true;
        }
    }

}
