namespace upp.Dtos.Calendar
{
    public class FindCalendarsDto
    {
        public DateTime? Date { get; set; }
        public int UserId { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}
