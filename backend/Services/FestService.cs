using backend.Data;
using backend.DTOs;
using backend.Models;
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
            // 1️⃣ Basic fest validation
            if (dto.EndDate < dto.StartDate)
                throw new Exception("End date cannot be before start date");

            // 2️⃣ Validate sessions
            foreach (var session in dto.Sessions)
            {
                if (session.Date < dto.StartDate || session.Date > dto.EndDate)
                    throw new Exception("Session date must be within fest duration");

                if (!TimeSpan.TryParse(session.StartTime, out var sessionStart))
                    throw new Exception($"Invalid session start time: {session.StartTime}");

                if (!TimeSpan.TryParse(session.EndTime, out var sessionEnd))
                    throw new Exception($"Invalid session end time: {session.EndTime}");

                if (sessionEnd <= sessionStart)
                    throw new Exception("Session end time must be after start time");
            }

            // 3️⃣ Validate meal slots
            foreach (var slot in dto.MealSlots)
            {
                if (slot.Date < dto.StartDate || slot.Date > dto.EndDate)
                    throw new Exception("Meal slot date must be within fest duration");

                if (!TimeSpan.TryParse(slot.StartTime, out var slotStart))
                    throw new Exception($"Invalid meal slot start time: {slot.StartTime}");

                if (!TimeSpan.TryParse(slot.EndTime, out var slotEnd))
                    throw new Exception($"Invalid meal slot end time: {slot.EndTime}");

                if (slotEnd <= slotStart)
                    throw new Exception("Meal slot end time must be after start time");
            }

            // 4️⃣ Create Fest entity
            var fest = new Fest
            {
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,

                Sessions = dto.Sessions.Select(s => new Session
                {
                    Date = s.Date,
                    StartTime = TimeSpan.Parse(s.StartTime),
                    EndTime = TimeSpan.Parse(s.EndTime)
                }).ToList(),

                MealSlots = dto.MealSlots.Select(m => new MealSlot
                {
                    Date = m.Date,
                    MealType = m.MealType switch
                    {
                        "Breakfast" => MealType.Breakfast,
                        "Lunch" => MealType.Lunch,
                        "EveningSnacks" => MealType.EveningSnacks,
                        "Dinner" => MealType.Dinner,
                        _ => throw new Exception($"Invalid meal type: {m.MealType}")
                    },
                    StartTime = TimeSpan.Parse(m.StartTime),
                    EndTime = TimeSpan.Parse(m.EndTime)
                }).ToList(),

                IsActive = true
            };

            // 5️⃣ Save fest (single atomic insert)
            await _context.Fests.InsertOneAsync(fest);

            return fest;
        }
    }
}
