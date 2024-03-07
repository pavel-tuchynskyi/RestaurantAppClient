using CommunityToolkit.Maui.Views;

namespace RestaurantAppClient.Popups;

public partial class PickerPopup : Popup
{
	public IEnumerable<string> Items { get; set; }
    public PickerPopup(IEnumerable<string> items)
	{
        Items = items;

        InitializeComponent();
	}

    private async void ItemsCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (string)e.CurrentSelection.FirstOrDefault();
        await CloseAsync(selectedItem);
    }
}