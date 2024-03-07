using CommunityToolkit.Maui.Views;

namespace RestaurantAppClient.Popups;

public partial class ExceptionPopup : Popup
{
	public ExceptionPopup(string message)
	{
		InitializeComponent();
        MessageLable.Text = message;
    }

    private async void CloseBtn_Clicked(object sender, EventArgs e)
    {
		await CloseAsync();
    }
}