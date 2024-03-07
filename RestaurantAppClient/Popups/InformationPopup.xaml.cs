using CommunityToolkit.Maui.Views;

namespace RestaurantAppClient.Popups;

public partial class InformationPopup : Popup
{
	public InformationPopup(string message)
	{
		InitializeComponent();

        MessageLable.Text = message;
    }

    private async void OkBtn_Clicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }
}