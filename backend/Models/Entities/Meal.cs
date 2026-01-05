namespace backend.Models.Entities
{
    public class MealSlot
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime Date { get; set; }
        public MealType MealType { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }


    public class MealConsumption
    {
        public string TicketCode { get; set; } = string.Empty;
        public string MealSlotId { get; set; } = string.Empty;
        public DateTime ConsumedAt { get; set; }
    }


}