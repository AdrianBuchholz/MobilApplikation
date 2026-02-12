using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Models;
using MyMauiApp.Services;



namespace MyMauiApp.ViewModels
{
    public partial class ConcertViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IApiService _api;
        public ObservableCollection<PerformanceDto> Performances { get; } = new();
        public int ConcertId { get; set; }
        public string Title { get; set; } = string.Empty;

        public ConcertViewModel(IApiService api)
        {
            _api = api;
            LoadCommand = new AsyncRelayCommand(LoadAsync);
            BookCommand = new AsyncRelayCommand<PerformanceDto>(BookAsync);
        }

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

        private async Task LoadAsync()
        {
            var items = await _api.GetPerformancesAsync(ConcertId);
            Performances.Clear();
            foreach (var p in items) Performances.Add(p);
        }

        private async Task BookAsync(PerformanceDto? perf)
        {
            if (perf == null) return;
            var mainPage = Application.Current?.MainPage;
            if (mainPage == null) return;

            var name = await mainPage.DisplayPromptAsync("Booking", "Your name:");
            if (string.IsNullOrWhiteSpace(name)) return;
            var email = await mainPage.DisplayPromptAsync("Booking", "Your email:");
            if (string.IsNullOrWhiteSpace(email)) return;

            var booking = new BookingDto { PerformanceId = perf.Id, Name = name, Email = email };
            var created = await _api.CreateBookingAsync(booking);
            if (created != null) await mainPage.DisplayAlert("Success", "Booking created", "OK");
        }
    }
}
