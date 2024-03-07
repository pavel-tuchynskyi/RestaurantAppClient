using Android.App;
using Android.Runtime;
using Microsoft.Maui.Handlers;
using RestaurantAppClient.Controls;
using RestaurantAppClient.Platforms.Android;

namespace RestaurantAppClient
{
    [Application(UsesCleartextTraffic = true)]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp()
        {
            Microsoft.Maui.Handlers.ElementHandler.ElementMapper.AppendToMapping("Bordered", (handler, view) =>
            {
                if (view is BorderedEntry)
                {
                    BorderedEntryHandler.Map(handler, view);
                }
            });

            return MauiProgram.CreateMauiApp();
        }
    }
}