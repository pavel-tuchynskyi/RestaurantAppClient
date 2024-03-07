using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models;
using RestaurantAppClient.Popups;

namespace RestaurantAppClient.Services
{
    public class AlertService : IAlertService
    {
        private readonly IPopupService _popupService;

        public AlertService(IPopupService popupService)
        {
            _popupService = popupService;
        }
        public async Task DisplayResponseAlert(ResponseException exception, string cancel)
        {
            if (Application.Current?.MainPage == null)
            {
                return;
            }

            await _popupService.ShowPopup(new ExceptionPopup(exception.Detail));
        }

        public async Task DisplayAlertMessage(string message)
        {
            if (Application.Current?.MainPage == null)
            {
                return;
            }

            await _popupService.ShowPopup(new ExceptionPopup(message));
        }
    }
}
