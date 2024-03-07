using Android.App;
using Android.Content.PM;
using Android.Content;
using Android.OS;
using RestaurantAppClient.Pages;
using System.Web;

namespace RestaurantAppClient
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.ActionView, Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "http", "https" }, DataHost = "restaurant-app.com", DataPathPrefix = "/ConfirmEmail",
        AutoVerify = true)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var uri = Intent?.Data;

            if (uri != null)
            {
                var query = HttpUtility.ParseQueryString(uri.EncodedQuery);

                var parameters = new Dictionary<string, object>
                {
                    { "id", query["id"] },
                    { "token", query["token"] }
                };

                Shell.Current.GoToAsync($"//{nameof(EmailConfirmedPage)}", false, parameters);
            }
        }
    }
}