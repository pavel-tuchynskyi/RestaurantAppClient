using RestaurantAppClient.ViewModels;

namespace RestaurantAppClient.Pages;

public partial class EmailConfirmedPage : ContentPage
{
    public EmailConfirmedPage(EmailConfirmedViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}