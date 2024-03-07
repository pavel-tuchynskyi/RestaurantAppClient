using CommunityToolkit.Maui.Views;
using RestaurantAppClient.Common.Interfaces;

namespace RestaurantAppClient.Services
{
    public class PopupService : IPopupService
    {
        public async Task ShowPopup(Popup popup)
        {
            Page page = Application.Current?.MainPage;
            await page.ShowPopupAsync(popup);
        }
    }
}
