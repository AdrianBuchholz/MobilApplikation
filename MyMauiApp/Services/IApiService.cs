using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMauiApp.Models;

namespace MyMauiApp.Services
{
    public interface IApiService
    {
        Task<IEnumerable<ConcertDto>> GetConcertsAsync();
        Task<IEnumerable<PerformanceDto>> GetPerformancesAsync(int concertId);
        Task<BookingDto?> CreateBookingAsync(BookingDto booking);
        Task<bool> DeleteBookingAsync(int id);
    }
}
