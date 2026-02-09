namespace MobilApplikation.Dtos
{
    public class PerformanceDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = string.Empty;
        public int ConcertId { get; set; }
        public int BookingCount { get; set; }
    }
}
