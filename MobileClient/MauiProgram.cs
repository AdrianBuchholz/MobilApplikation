using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.Messaging;
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

        builder.Services.AddSingleton<IApiService, ApiService>(sp => new ApiService("https://localhost:5001"));
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<ConcertViewModel>();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}