using System.Diagnostics;
using System.Windows.Input;

namespace RestaurantAppClient.Views;

public partial class FoodMenuView : ContentView
{
	public static BindableProperty CategoryItemSourceProperty =
		BindableProperty.Create(nameof(CategoryItemSource), typeof(IEnumerable<string>), typeof(FoodMenuView), propertyChanged: OnCategoryItemSourceChanged);

	public IEnumerable<string> CategoryItemSource
	{
        get => (IEnumerable<string>)GetValue(CategoryItemSourceProperty);
        set => SetValue(CategoryItemSourceProperty, value);
    }

	public static BindableProperty MenuImageSourceProperty =
		BindableProperty.Create(nameof(MenuImageSource), typeof(string), typeof(FoodMenuView), propertyChanged: OnMenuImageSourceChanged);

	public string MenuImageSource
    {
        get => (string)GetValue(MenuImageSourceProperty);
        set => SetValue(MenuImageSourceProperty, value);
	}

	public static BindableProperty MenuTitleProperty =
		BindableProperty.Create(nameof(MenuTitle), typeof(string), typeof(FoodMenuView), propertyChanged: OnMenuTitleChanged);

	public string MenuTitle
	{
        get => (string)GetValue(MenuTitleProperty);
        set => SetValue(MenuTitleProperty, value);
	}

    public static BindableProperty CategorySelectedCommandProperty =
        BindableProperty.Create(nameof(CategorySelectedCommand), typeof(ICommand), typeof(FoodMenuView));

    public ICommand CategorySelectedCommand
    {
        get => (ICommand)GetValue(CategorySelectedCommandProperty);
        set => SetValue(CategorySelectedCommandProperty, value);
    }

    private Grid _categoryGrid;

	public FoodMenuView()
	{
		InitializeComponent();
		BindingContext = this;
    }

	private static void OnCategoryItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
	{
        var view = (FoodMenuView)bindable;
        view.InitilizeCategoryGrid((IEnumerable<string>)newValue);
    }

    private static void OnMenuImageSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (FoodMenuView)bindable;
        view.ChangeMenuImage((string)newValue);
    }

    private void ChangeMenuImage(string newValue)
    {
        MenuImage.Source = ImageSource.FromFile(newValue);
    }

    private static void OnMenuTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var view = (FoodMenuView)bindable;
        view.ChangeTitle((string)newValue);
    }

    private void ChangeTitle(string newValue)
	{
        TitleLabel.Text = (string)newValue;
    }

    private void InitilizeCategoryGrid(IEnumerable<string> newValue)
	{
        var items = newValue.ToList();

		if(!items.Any())
		{
			return;
		}

        _categoryGrid = new Grid()
		{
            BackgroundColor = Color.FromArgb("#7077A1"),
			IsVisible = false
        };

        var rowsCount = items.Count <= 4 ? 2 : items.Count / 2;
		var columnsCount = 2;

        CreateGridCells(_categoryGrid, rowsCount, columnsCount);

        FillGrid(_categoryGrid, items, rowsCount, columnsCount);

		MenuGrid.Add(_categoryGrid, 0, 0);
    }

	private void CreateGridCells(Grid grid, int rows, int columns)
	{
        for (int i = 0; i < rows; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition()
            {
                Height = new GridLength(1, GridUnitType.Star)
            });

        }

        for (int i = 0; i < columns; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition());
        }
    }

	private void FillGrid(Grid grid, List<string> items, int rows, int columns)
	{
		if(rows * columns < items.Count)
		{
			Debug.WriteLine("Items count need to be less or equal to rows * column");
			return;
		}

		//Fill grid cells with category names
        for (int i = 0; i < items.Count; i++)
        {
			var row = i / columns;
			var col = i % columns;

			var border = new Border()
			{
				Stroke = Colors.Transparent,
				BackgroundColor = Color.FromArgb("#232D3F"),
				Content = new Label()
				{
					Text = items[i],
					FontSize = 20,
					TextColor = Color.FromArgb("#7077A1"),
					FontAttributes = FontAttributes.Bold,
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center
				}
			};

			border.GestureRecognizers.Add(new TapGestureRecognizer() 
			{
				Command = new Command(() => SelectCategory(((Label)border.Content).Text))
			});

            grid.Add(border, row, col);
        }

		//Fill empty grid cells
		if(items.Count < rows * columns)
		{
            for (int i = items.Count; i < rows * columns; i++)
            {
                var row = i / columns;
                var col = i % columns;

                grid.Add(new Border()
                {
                    Stroke = Colors.Transparent,
                    BackgroundColor = Color.FromArgb("#232D3F")
                }, row, col);
            }
        }
    }

    private void ShowCategories(object sender, TappedEventArgs e)
    {
        _categoryGrid.IsVisible = true;
    }

    void SelectCategory(string category)
    {
        CategorySelectedCommand.Execute((MenuTitle, category));
    }

    public void HideCategories()
    {
        _categoryGrid.IsVisible = false;
    }
}