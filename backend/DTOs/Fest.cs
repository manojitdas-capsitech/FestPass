namespace backend.DTOs
{
    public class CreateFestDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public class CreateFestDayDto
    {
        public int DayNumber { get; set; }
        public DateTime Date { get; set; }
    }


    public class CreateSessionDto
    {
        public int DayNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }

    public class CreateMealSlotDto
    {
        public int DayNumber { get; set; }
        public string MealType { get; set; } = string.Empty;
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }


}