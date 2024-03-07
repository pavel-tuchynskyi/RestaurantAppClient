using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models.Account;
using RestaurantAppClient.Pages;

namespace RestaurantAppClient.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly ApiClient _apiClient;
        private readonly IAlertService _alertService;
        private readonly ITokenService _tokenService;

        [ObservableProperty]
        private UserLogin _user = new();

        public LoginViewModel(IClientFactory clientFactory, IAlertService alertService, ITokenService tokenService) 
        {
            _apiClient = clientFactory.CreateClient();
            _alertService = alertService;
            _tokenService = tokenService;
        }

        [RelayCommand]
        public async Task Login()
        {
            if (!IsValid(_user, out var errors))
            {
                var errorString = String.Join("\r\n", errors);
                await _alertService.DisplayAlertMessage(errorString);
                return;
            }

            var response = await ExecuteWithLoadingIndicator(async() => await _apiClient.LoginUserAsync(_user));

            if (response.Exception != null)
            {
                await _alertService.DisplayResponseAlert(response.Exception, "Ok");
            }
            else
            {
                await _tokenService.SetAccessToken(response.Data.Value);
                await Shell.Current?.GoToAsync($"///{nameof(MenuPage)}");
            }
        }
    }
}
