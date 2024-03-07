using RestaurantAppClient.ViewModels;

namespace RestaurantAppClient.Pages;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}