namespace backend.Models.Entities
{
    public class ScanLog
    {
        public string TicketCode { get; set; } = string.Empty;
        public ScanType ScanType { get; set; }
        public DateTime ScannedAt { get; set; }
        public string ScannedBy { get; set; } = string.Empty; // Staff ID
    }

}
