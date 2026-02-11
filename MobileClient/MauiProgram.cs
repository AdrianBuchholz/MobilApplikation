using Microsoft.Extensions.Logging;
using MobileClient.Services;
using MobileClient.ViewModels;

namespace MobileClient;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<IApiService>(sp => new ApiService("https://10.0.2.2:5001"));
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<ConcertViewModel>();

        // Register pages for navigation (use transient so new VM/page created each navigation)
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ConcertPage>();

        // ApiService registered above

        // debug logging is provided by platform
        return builder.Build();
    }
}