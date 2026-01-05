namespace backend.DTOs
{
    public class ScanRequestDto
    {
        public string TicketCode { get; set; } = string.Empty;
        public string FestId { get; set; } = string.Empty;

    }

    public class ScanResponseDto
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
    }


}