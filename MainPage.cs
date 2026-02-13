MyMauiApp/MainPage.xaml.cs
namespace MyMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnBookingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("BookingsPage");
    }
}