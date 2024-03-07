using CommunityToolkit.Maui.Core.Platform;

namespace RestaurantAppClient.Controls;

public class BorderedEntry : Entry
{
    public static readonly BindableProperty BorderRadiusProperty =
            BindableProperty.Create(nameof(BorderRadius), typeof(int), typeof(BorderedEntry), 0); 

    public static BindableProperty BorderThicknessProperty =
		BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(BorderedEntry), 0);

    public static BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(BorderedEntry), Colors.Black);

    public static BindableProperty BorderPaddingProperty =
        BindableProperty.Create(nameof(BorderPadding), typeof(Thickness), typeof(BorderedEntry), Thickness.Zero);

    public int BorderRadius
    {
        get { return (int)GetValue(BorderRadiusProperty); }
        set { SetValue(BorderRadiusProperty, value); }
    }

    public int BorderThickness
    {
        get => (int)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public Thickness BorderPadding
    {
        get => (Thickness)GetValue(BorderPaddingProperty);
        set => SetValue(BorderPaddingProperty, value);
    }

    public BorderedEntry()
	{
        Focused += Entry_Focused;

    }

    private async void Entry_Focused(object sender, FocusEventArgs e)
    {
        if (DeviceInfo.Current.Platform != DevicePlatform.MacCatalyst)
        {
            await KeyboardExtensions.ShowKeyboardAsync((Entry)sender, default);
        }
    }
}