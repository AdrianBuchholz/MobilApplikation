namespace MobilApplikation.Models
{
    public class Performance
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; } = "";
        public int ConcertId { get; set; }

        // Navigation properties
        public Concert? Concert { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

        // Additional properties can be added as needed
    }
}
