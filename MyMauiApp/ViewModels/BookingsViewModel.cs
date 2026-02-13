using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMauiApp.Models;
using MyMauiApp.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace MyMauiApp.ViewModels
{
    public partial class BookingsViewModel : ObservableObject
    {
        private readonly IApiService _api;
        public ObservableCollection<BookingDto> Bookings { get; } = new();

        public BookingsViewModel(IApiService api)
        {
            _api = api;
            LoadCommand = new AsyncRelayCommand(LoadAsync);
            DeleteCommand = new AsyncRelayCommand<BookingDto>(DeleteAsync);
        }

        public IAsyncRelayCommand LoadCommand { get; }
        public IAsyncRelayCommand<BookingDto> DeleteCommand { get; }

        private async Task LoadAsync()
        {
            var items = await _api.GetBookingsAsync();
            Bookings.Clear();
            foreach (var b in items) Bookings.Add(b);
        }

        private async Task DeleteAsync(BookingDto? booking)
        {
            if (booking == null) return;
            var mainPage = Application.Current?.MainPage;
            if (mainPage == null) return;

            var confirm = await mainPage.DisplayAlert("Delete", $"Delete booking by {booking.Name}?", "Yes", "No");
            if (!confirm) return;

            var success = await _api.DeleteBookingAsync(booking.Id);
            if (success)
            {
                Bookings.Remove(booking);
                await mainPage.DisplayAlert("Deleted", "Booking deleted", "OK");
            }
            else
            {
                await mainPage.DisplayAlert("Error", "Failed to delete booking", "OK");
            }
        }
    }
}
