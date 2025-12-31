namespace backend.Models.Entities
{
    public class Session
    {
        public int DayNumber { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
