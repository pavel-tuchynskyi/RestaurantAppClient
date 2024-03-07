using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantAppClient.ViewModels;
using RestaurantAppClient.Views;

namespace RestaurantAppClient.Pages;

public partial class MenuPage : ContentPage
{
    private readonly MenuPageViewModel _viewModel;

    public MenuPage(MenuPageViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.Initialize();
    }

    protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        var childElements = MenuCarousel.GetVisualTreeDescendants();
        foreach (var child in childElements)
        {
            if (child is FoodMenuView menu)
                menu.HideCategories();
        }

        base.OnNavigatedFrom(args);
    }
}