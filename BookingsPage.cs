MyMauiApp/BookingsPage.xaml.cs
using MyMauiApp.ViewModels;

namespace MyMauiApp;

public partial class BookingsPage : ContentPage
{
    public BookingsPage(BookingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        _ = vm.LoadCommand.ExecuteAsync(null);
    }
}