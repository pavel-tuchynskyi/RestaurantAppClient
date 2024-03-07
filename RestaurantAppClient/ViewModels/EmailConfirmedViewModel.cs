using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Pages;

namespace RestaurantAppClient.ViewModels
{
    public partial class EmailConfirmedViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IAlertService _alertService;
        private readonly ApiClient _apiClient;

        [ObservableProperty]
        private bool _isConfirmed;

        public EmailConfirmedViewModel(IAlertService alertService, IClientFactory clientFactory)
        {
            _alertService = alertService;
            _apiClient = clientFactory.CreateClient();
        }

        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var id = query["id"] as string;
            var token = query["token"] as string;

            await ConfirmEmail(id, token);
        }

        private async Task ConfirmEmail(string id, string token)
        {
            var response = await ExecuteWithLoadingIndicator(async () => await _apiClient.ConfirmEmail(id, token));

            if (response.Exception != null)
            {
                await _alertService.DisplayResponseAlert(response.Exception, "Ok");
            }
            else
            {
                IsConfirmed = true;
            }

            await Shell.Current?.GoToAsync(nameof(MainPage), false);
        }
    }
}
