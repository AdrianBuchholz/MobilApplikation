using MobileClient.Models;

namespace MobileClient.Services;

public interface IApiService
{
    Task<IEnumerable<ConcertDto>> GetConcertsAsync();
    Task<IEnumerable<PerformanceDto>> GetPerformancesAsync(int concertId);
    Task<BookingDto?> CreateBookingAsync(BookingDto booking);
    Task<bool> DeleteBookingAsync(int id);
}