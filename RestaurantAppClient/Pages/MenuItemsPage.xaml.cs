using RestaurantAppClient.ViewModels;

namespace RestaurantAppClient.Pages;

public partial class MenuItemsPage : ContentPage
{
    private readonly MenuItemsViewModel _viewModel;

    public MenuItemsPage(MenuItemsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.Initialize();
    }
}