using MyMauiApp.ViewModels;

namespace MyMauiApp
{
    public partial class App : Application
    {
        public App(MainPage mainPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage(mainPage); // DI resolves MainPage with MainViewModel
        }
    }
}