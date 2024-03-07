using RestaurantAppClient.Models.Menu;
using System.Windows.Input;

namespace RestaurantAppClient.Views;

public partial class FoodItemView : ContentView
{
    public static BindableProperty OrderBthCommandProperty =
        BindableProperty.Create(nameof(OrderBthCommand), typeof(ICommand), typeof(DrinkItemView));

    public ICommand OrderBthCommand
    {
        get => (ICommand)GetValue(OrderBthCommandProperty);
        set => SetValue(OrderBthCommandProperty, value);
    }

    public FoodItemView()
	{
		InitializeComponent();
	}

    private void OrderBtn_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        OrderBthCommand.Execute((MenuFoodItem)button.CommandParameter);
    }
}