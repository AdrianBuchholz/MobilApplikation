using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMauiApp.Models;
using MyMauiApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MyMauiApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IApiService _api;
        public ObservableCollection<ConcertDto> Concerts { get; } = new();
        private ConcertDto? _selectedConcert;

        public MainViewModel(IApiService api)
        {
            _api = api;
            LoadCommand = new AsyncRelayCommand(LoadAsync);
        }

        public IAsyncRelayCommand LoadCommand { get; }

        public ConcertDto? SelectedConcert
        {
            get => _selectedConcert;
            set
            {
                if (SetProperty(ref _selectedConcert, value) && value != null)
                {
                    // navigate to concert page using NavigationPage (avoid Shell dependency)
                    async Task NavigateAsync()
                    {
                        var page = new ConcertPage();
                        var vm = new ConcertViewModel(_api);
                        // set concert id and trigger load
                        vm.ConcertId = value.Id;
                        page.BindingContext = vm;
                        vm.ApplyQueryAttributes(new Dictionary<string, object> { { "concertId", value.Id.ToString() } });
                        if (Application.Current?.MainPage?.Navigation != null)
                            await Application.Current.MainPage.Navigation.PushAsync(page);
                    }

                    _ = NavigateAsync();
                    // clear selection
                    SelectedConcert = null;
                }
            }
        }

        private async Task LoadAsync()
        {
            var items = await _api.GetConcertsAsync();
            Concerts.Clear();
            foreach (var c in items) Concerts.Add(c);
        }
    }
}
