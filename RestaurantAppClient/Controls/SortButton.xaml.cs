using CommunityToolkit.Maui.Views;
using RestaurantAppClient.Popups;
using System.Windows.Input;

namespace RestaurantAppClient.Controls;

public partial class SortButton : ImageButton
{
	public static BindableProperty SortOptionsProperty =
		BindableProperty.Create(nameof(SortOptions), typeof(IEnumerable<string>), typeof(SortButton));

	public IEnumerable<string> SortOptions
	{
		get => (IEnumerable<string>)GetValue(SortOptionsProperty);
		set => SetValue(SortOptionsProperty, value);
	}

	public static BindableProperty SelectedOptionsChangedCommandProperty =
		BindableProperty.Create(nameof(SelectedOptionsChangedCommand), typeof(ICommand), typeof(SortButton));

    public ICommand SelectedOptionsChangedCommand
    {
        get => (ICommand)GetValue(SelectedOptionsChangedCommandProperty);
        set => SetValue(SelectedOptionsChangedCommandProperty, value);
    }

    public SortButton()
	{
		InitializeComponent();
    }

    private async void SortOptionsBtn_Clicked(object sender, EventArgs e)
    {
        Page page = Application.Current?.MainPage;
        var result = await page.ShowPopupAsync(new PickerPopup(SortOptions));

        if (result is string pickedOption 
            && SelectedOptionsChangedCommand.CanExecute(pickedOption))
        {
            SelectedOptionsChangedCommand.Execute(pickedOption);
        }
    }
}