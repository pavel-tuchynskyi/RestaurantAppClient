namespace RestaurantAppClient.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
	{
		InitializeComponent();
    }

    private async void LoginBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current?.GoToAsync(nameof(LoginPage));
    }

    private async void RegisterBtn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current?.GoToAsync(nameof(RegistrationPage));
    }
}