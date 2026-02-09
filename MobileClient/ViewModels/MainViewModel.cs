using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MobileClient.Models;
using MobileClient.Services;
using System.Collections.ObjectModel;

namespace MobileClient.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IApiService _api;
    public ObservableCollection<ConcertDto> Concerts { get; } = new();
n    public MainViewModel(IApiService api)
    {
        _api = api;
        LoadCommand = new AsyncRelayCommand(LoadAsync);
    }
n    public IAsyncRelayCommand LoadCommand { get; }
n    private async Task LoadAsync()
    {
        var items = await _api.GetConcertsAsync();
        Concerts.Clear();
        foreach (var c in items) Concerts.Add(c);
    }
n    [ICommand]
    private async Task SelectConcert(ConcertDto concert)
    {
        if (concert == null) return;
        await Shell.Current.GoToAsync($"/concert?concertId={concert.Id}");
    }
}