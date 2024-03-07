using RestaurantAppClient.Models.Menu;
using RestaurantAppClient.ViewModels;
using RestaurantAppClient.Views;

namespace RestaurantAppClient.Pages;

public partial class MenuItemDetailsPage : ContentPage
{
	private readonly MenuItemDetailsViewModel _viewModel;
    public MenuItemDetailsPage(MenuItemDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (!_viewModel.IsLoaded)
        {
            await _viewModel.Initialize();

            SetItemView();
        }
    }

    private void SetItemView()
    {
        if (_viewModel.MenuItem is MenuDrinkItem)
        {
            Self.Content = new DrinkItemView()
            {
                BindingContext = _viewModel.MenuItem,
                OrderBthCommand = _viewModel.AddToCartCommand
            };
        }
        else
        {
            Self.Content = new FoodItemView()
            {
                BindingContext = _viewModel.MenuItem,
                OrderBthCommand = _viewModel.AddToCartCommand
            };
        }
    }
}