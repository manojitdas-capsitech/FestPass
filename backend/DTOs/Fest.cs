using backend.Models;

namespace backend.DTOs
{
    public class CreateFestDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<CreateSessionDto> Sessions { get; set; } = new();
        public List<CreateMealSlotDto> MealSlots { get; set; } = new();
    }


    public class CreateSessionDto
    {
        public DateTime Date { get; set; }
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }



    public class CreateMealSlotDto
    {
        public DateTime Date { get; set; }
        public string MealType { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
    }




}