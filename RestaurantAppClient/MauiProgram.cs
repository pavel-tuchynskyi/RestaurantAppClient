using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestaurantAppClient.Configuration;
using RestaurantAppClient.Handlers.SearchBar;
using System.Reflection;

namespace RestaurantAppClient
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("TiltWarp-Regular.ttf", "TiltWarp");
                    fonts.AddFont("icomoon.ttf", "Icons");
                })
                .ConfigureMauiHandlers(handlers =>
                {
                    handlers.AddHandler<SearchBar, SearchBarExHandler>();
                })
                .UseMauiMaps();

            var a = Assembly.GetExecutingAssembly();
            using var stream = a.GetManifestResourceStream("RestaurantAppClient.appsettings.json");

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();

            builder.Configuration.AddConfiguration(config);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services
                .AddViews()
                .AddServices();

            return builder.Build();
        }
    }
}