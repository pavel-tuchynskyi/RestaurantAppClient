namespace RestaurantAppClient.Pages;

[QueryProperty(nameof(Email), nameof(Email))]
[QueryProperty(nameof(FirstName), nameof(FirstName))]
public partial class EmailConfirmationPage : ContentPage
{
	private string _email;
	public string Email
	{
		get => _email;
		set
		{
           _email = value;
			OnPropertyChanged();
        }
	}

	private string _firstName;
	public string FirstName
	{
		get => _firstName;
		set
		{
			_firstName = value;
			OnPropertyChanged();
		}
	}

    public EmailConfirmationPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private async void ReturnBtn_Clicked(object sender, EventArgs e)
    {
		await Shell.Current?.GoToAsync($"///{nameof(MainPage)}");
    }
}