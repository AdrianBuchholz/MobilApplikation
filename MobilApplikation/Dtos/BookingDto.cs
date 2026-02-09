namespace MobilApplikation.Dtos
{
    public class BookingDto
    {
        public int Id { get; set; }
        public int PerformanceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
