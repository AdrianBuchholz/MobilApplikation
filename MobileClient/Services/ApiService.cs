using MobileClient.Models;
using System.Net.Http.Json;

namespace MobileClient.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _http;
    public ApiService(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
    }
n    public async Task<IEnumerable<ConcertDto>> GetConcertsAsync() =>
        await _http.GetFromJsonAsync<IEnumerable<ConcertDto>>("/api/concerts") ?? Enumerable.Empty<ConcertDto>();
n    public async Task<IEnumerable<PerformanceDto>> GetPerformancesAsync(int concertId) =>
        await _http.GetFromJsonAsync<IEnumerable<PerformanceDto>>($"/api/performances/{concertId}") ?? Enumerable.Empty<PerformanceDto>();
n    public async Task<BookingDto?> CreateBookingAsync(BookingDto booking)
    {
        var res = await _http.PostAsJsonAsync("/api/bookings", booking);
        return res.IsSuccessStatusCode ? await res.Content.ReadFromJsonAsync<BookingDto>() : null;
    }
n    public async Task<bool> DeleteBookingAsync(int id)
    {
        var res = await _http.DeleteAsync($"/api/bookings/{id}");
        return res.IsSuccessStatusCode;
    }
}