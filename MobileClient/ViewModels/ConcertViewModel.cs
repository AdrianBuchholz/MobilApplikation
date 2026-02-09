using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileClient.Models;
using MobileClient.Services;
using System.Collections.ObjectModel;

namespace MobileClient.ViewModels;

public partial class ConcertViewModel : ObservableObject, IQueryAttributable
{
    private readonly IApiService _api;
    public ObservableCollection<PerformanceDto> Performances { get; } = new();
    public int ConcertId { get; set; }
    public string Title { get; set; } = string.Empty;

    public ConcertViewModel(IApiService api) { _api = api; LoadCommand = new AsyncRelayCommand(LoadAsync); BookCommand = new AsyncRelayCommand<PerformanceDto>(BookAsync); }

    public IAsyncRelayCommand LoadCommand { get; }
    public IAsyncRelayCommand<PerformanceDto> BookCommand { get; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("concertId", out var idObj) && idObj is string idStr && int.TryParse(idStr, out var id))
        {
            ConcertId = id;
            _ = LoadAsync();
        }
    }
n    private async Task LoadAsync()
    {
        var items = await _api.GetPerformancesAsync(ConcertId);
        Performances.Clear();
        foreach (var p in items) Performances.Add(p);
    }

    private async Task BookAsync(PerformanceDto? perf)
    {
        if (perf == null) return;
        var name = await Application.Current.MainPage.DisplayPromptAsync("Booking", "Your name:");
        if (string.IsNullOrWhiteSpace(name)) return;
        var email = await Application.Current.MainPage.DisplayPromptAsync("Booking", "Your email:");
        if (string.IsNullOrWhiteSpace(email)) return;

        var booking = new BookingDto { PerformanceId = perf.Id, Name = name, Email = email };
        var created = await _api.CreateBookingAsync(booking);
        if (created != null) await Application.Current.MainPage.DisplayAlert("Success", "Booking created", "OK");
    }
}