using RestaurantAppClient.Common.Interfaces;

namespace RestaurantAppClient.Pages;

public partial class LoadingPage : ContentPage
{
    private readonly ITokenService _tokenService;

    public LoadingPage(ITokenService tokenService)
	{
		InitializeComponent();
        _tokenService = tokenService;
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        await IsAuthenticated();
    }

    private async Task IsAuthenticated()
    {
        var token = await _tokenService.GetAccessToken();
        if (_tokenService.CheckTokenValidity(token))
        {
            await Shell.Current?.GoToAsync($"///{nameof(MenuPage)}");
        }
        else
        {
            await Shell.Current?.GoToAsync($"///{nameof(MainPage)}");
        }
    }
}