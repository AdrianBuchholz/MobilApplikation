using MyMauiApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MyMauiApp.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        public ApiService(string baseUrl)
        {
            _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<IEnumerable<ConcertDto>> GetConcertsAsync() =>
            await _http.GetFromJsonAsync<IEnumerable<ConcertDto>>("/api/concerts") ?? Enumerable.Empty<ConcertDto>();

        public async Task<IEnumerable<PerformanceDto>> GetPerformancesAsync(int concertId) =>
            await _http.GetFromJsonAsync<IEnumerable<PerformanceDto>>($"/api/performances/{concertId}") ?? Enumerable.Empty<PerformanceDto>();

        public async Task<BookingDto?> CreateBookingAsync(BookingDto booking)
        {
            var res = await _http.PostAsJsonAsync("/api/bookings", booking);
            return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<BookingDto>() : null;
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            var res = await _http.DeleteAsync($"/api/bookings/{id}");
            return res.IsSuccessStatusCode;
        }
    }
}
