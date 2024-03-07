using CommunityToolkit.Maui.Core.Platform;
using RestaurantAppClient.ViewModels;

namespace RestaurantAppClient.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private async void Entry_Focused(object sender, FocusEventArgs e)
    {
		await KeyboardExtensions.ShowKeyboardAsync((Entry)sender, default);
    }
}