using MobileClient.ViewModels;

namespace MobileClient;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new ContentPage
        {
            Content = new Label
            {
                Text = "Hello, MobileClient is running!",
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            }
        };
    }
}