using Microsoft.Extensions.Logging;
using MyMauiApp.Services;
using MyMauiApp.ViewModels;

namespace MyMauiApp
{
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

#if ANDROID
            string apiUrl = "http://10.0.2.2:5000";
#else
        string apiUrl = "http://localhost:5000";
#endif

            builder.Services.AddSingleton<IApiService>(sp => new ApiService(apiUrl));
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<ConcertViewModel>();

            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ConcertPage>();

            return builder.Build();
        }
    }
}
