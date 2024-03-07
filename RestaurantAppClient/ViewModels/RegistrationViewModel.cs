using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models.Account;
using RestaurantAppClient.Pages;

namespace RestaurantAppClient.ViewModels
{
    public partial class RegistrationViewModel : BaseViewModel
    {
        private readonly ApiClient _apiClient;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private UserRegister _user = new();

        public RegistrationViewModel(IClientFactory clientFactory, IAlertService alertService)
        {
            _apiClient = clientFactory.CreateClient();
            _alertService = alertService;
        }

        [RelayCommand]
        public async Task Register()
        {
            if (!IsValid(_user, out var errors))
            {
                var errorString = String.Join("\r\n", errors);
                await _alertService.DisplayAlertMessage(errorString);
                return;
            }

            var response = await ExecuteWithLoadingIndicator(async () => await _apiClient.RegisterUserAsync(_user));

            if (response.Exception != null)
            {
                await _alertService.DisplayResponseAlert(response.Exception, "Ok");
            }
            else
            {
                var parameters = new Dictionary<string, object>()
                {
                    { "FirstName", _user.FirstName },
                    { "Email", _user.Email }
                };

                await Shell.Current?.GoToAsync($"///{nameof(EmailConfirmationPage)}", true, parameters);
            }
        }
    }
}
