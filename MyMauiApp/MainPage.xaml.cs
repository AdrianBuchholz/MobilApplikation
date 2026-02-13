using MyMauiApp.ViewModels;

namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void OnBookingsClicked(object sender, EventArgs e)
    {
        // Prefer Navigation (works when app uses NavigationPage)
        var nav = Application.Current?.MainPage?.Navigation;
        if (nav != null)
        {
            // Try to resolve BookingsPage from DI
            BookingsPage? page = null;
            var services = Application.Current?.Handler?.MauiContext?.Services;
            if (services != null)
            {
                page = services.GetService(typeof(BookingsPage)) as BookingsPage;
            }

            page ??= new BookingsPage();
            await nav.PushAsync(page);
            return;
        }

        // Fallback to Shell navigation if Shell is available
        if (Shell.Current != null)
        {
            await Shell.Current.GoToAsync("BookingsPage");
            return;
        }

        // Last resort: create and set as MainPage
        var fallback = new NavigationPage(new BookingsPage());
        Application.Current.MainPage = fallback;
    }
}
