using RestaurantAppClient.Models.Menu;
using System.Windows.Input;

namespace RestaurantAppClient.Views;

public partial class DrinkItemView : ContentView
{
	public static BindableProperty OrderBthCommandProperty =
		BindableProperty.Create(nameof(OrderBthCommand), typeof(ICommand), typeof(DrinkItemView));

	public ICommand OrderBthCommand
	{
		get => (ICommand)GetValue(OrderBthCommandProperty);
		set => SetValue(OrderBthCommandProperty, value);
	}

    public DrinkItemView()
	{
		InitializeComponent();
	}

    private void OrderBtn_Clicked(object sender, EventArgs e)
    {
		var button = (Button)sender;
		OrderBthCommand.Execute((MenuDrinkItem)button.CommandParameter);
    }
}