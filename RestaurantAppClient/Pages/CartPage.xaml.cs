using RestaurantAppClient.ViewModels;

namespace RestaurantAppClient.Pages;

public partial class CartPage : ContentPage
{
	private readonly CartPageViewModel _viewModel;
    public CartPage(CartPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.Initialize();
    }
}