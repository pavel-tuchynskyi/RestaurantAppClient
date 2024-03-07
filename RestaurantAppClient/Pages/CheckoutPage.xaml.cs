using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using RestaurantAppClient.ViewModels;

namespace RestaurantAppClient.Pages;

public partial class CheckoutPage : ContentPage
{
    private readonly CheckoutViewModel _viewModel;

    public CheckoutPage(CheckoutViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await _viewModel.Initialize();
        SetDeliveryPin(_viewModel.OrderLocation);
    }

    private async void map_MapClicked(object sender, Microsoft.Maui.Controls.Maps.MapClickedEventArgs e)
    {
        await _viewModel.SetDeliveryAddress(e.Location);
        SetDeliveryPin(e.Location);
    }

    private async void BorderedEntry_Unfocused(object sender, FocusEventArgs e)
    {
        await _viewModel.SetDeliveryLocation();
        SetDeliveryPin(_viewModel.OrderLocation);
    }

    private void SetDeliveryPin(Location location)
    {
        map.Pins.Clear();
        map.Pins.Add(new Pin()
        {
            Location = location,
            Label = string.Empty
        });

        var mapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(0.444));
        map.MoveToRegion(mapSpan);
    }
}