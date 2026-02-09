using MobilApplikation.Models;

namespace MobilApplikation.Dtos
{
    public static class MappingExtensions
    {
        public static ConcertDto ToDto(this Concert c, int bookingCount) => new ConcertDto
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            BookingCount = bookingCount
        };

        public static PerformanceDto ToDto(this Performance p, int bookingCount) => new PerformanceDto
        {
            Id = p.Id,
            DateTime = p.DateTime,
            Location = p.Location,
            ConcertId = p.ConcertId,
            BookingCount = bookingCount
        };

        public static BookingDto ToDto(this Booking b) => new BookingDto
        {
            Id = b.Id,
            PerformanceId = b.PerformanceId,
            Name = b.Name,
            Email = b.Email
        };
    }
}
