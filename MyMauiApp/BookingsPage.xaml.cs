using MyMauiApp.ViewModels;
using MyMauiApp.Services;

namespace MyMauiApp;

public partial class BookingsPage : ContentPage
{
    // Constructor used by DI when resolving from service provider
    public BookingsPage(BookingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _ = vm.LoadCommand.ExecuteAsync(null);
    }

    // Parameterless constructor used by XAML/Shell; attempt to resolve VM from MAUI DI
    public BookingsPage()
    {
        InitializeComponent();

        // Try to get the MAUI service provider
        var services = Application.Current?.Handler?.MauiContext?.Services;
        BookingsViewModel? vm = null;
        if (services != null)
        {
            vm = services.GetService(typeof(BookingsViewModel)) as BookingsViewModel;
        }

        if (vm == null)
        {
            // Try to resolve IApiService, otherwise create a fallback ApiService using localhost
            IApiService? api = null;
            if (services != null)
            {
                api = services.GetService(typeof(IApiService)) as IApiService;
            }

            api ??= new ApiService("http://localhost:5000");
            vm = new BookingsViewModel(api);
        }

        BindingContext = vm;
        _ = vm.LoadCommand.ExecuteAsync(null);
    }
}