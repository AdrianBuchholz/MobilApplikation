using MobilApplikation.Models;

public static class PerformanceExtensions
{
    public static object ToDto(this Performance performance, int bookingCount)
    {
        return new
        {
            Id = performance.Id,
            DateTime = performance.DateTime,
            Location = performance.Location,
            ConcertId = performance.ConcertId,
            BookingCount = bookingCount
        };
    }
}