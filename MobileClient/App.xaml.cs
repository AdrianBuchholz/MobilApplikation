using MobileClient.ViewModels;

namespace MobileClient;

public partial class App : Application
{
    public App(MainViewModel mainVm)
    {
        InitializeComponent();
        MainPage = new NavigationPage(new MainPage { BindingContext = mainVm });
    }
}