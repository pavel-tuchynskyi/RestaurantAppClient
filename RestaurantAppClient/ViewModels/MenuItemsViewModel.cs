using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models;
using RestaurantAppClient.Models.Menu;
using RestaurantAppClient.Pages;
using System.Collections.ObjectModel;
using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.ViewModels
{
    public partial class MenuItemsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        private string _category;

        public ObservableCollection<MenuItem> MenuItems { get; set; } = new();

        [ObservableProperty]
        private string _search = string.Empty;

        [ObservableProperty]
        private string _orderBy = string.Empty;

        [ObservableProperty]
        private bool _isAscending = false;

        [ObservableProperty]
        private int _pageNumber = 1;

        [ObservableProperty]
        private int _pageSize = 5;

        [ObservableProperty]
        private int _totalPages = 1;

        [ObservableProperty]
        private int _recordsRemain = -1;

        [ObservableProperty]
        private MenuItem _selectedMenuItem;

        private string _menu;

        private readonly ApiClient _apiClient;
        private readonly IAlertService _alertService;

        public MenuItemsViewModel(IClientFactory clientFactory, IAlertService alertService)
        {
            _apiClient = clientFactory.CreateClient();
            _alertService = alertService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            _menu = query["Menu"] as string;
            Category = query["Category"] as string;
        }

        public async Task Initialize()
        {
            if (IsLoaded)
            {
                return;
            }

            await FetchMenuItems();
            IsLoaded = true;
        }

        public async Task FetchMenuItems()
        {
            var search = new Search(nameof(MenuFoodItem.Name), Search);
            var orderBy = new OrderBy(OrderBy, IsAscending);
            var paging = new Paging(PageNumber, PageSize);

            var response = await ExecuteWithLoadingIndicator(async () =>
                await _apiClient.GetMenuItems<MenuItem>(_menu, Category, search, orderBy, paging));

            if (response.Exception != null)
            {
                await _alertService.DisplayResponseAlert(response.Exception, "Ok");
            }
            else
            {
                foreach (var item in response.Data.Items)
                {
                    MenuItems.Add(item);
                }

                TotalPages = response.Data.TotalPages;
                var resordsRemain = response.Data.TotalRecords - (PageNumber * PageSize);
                RecordsRemain = resordsRemain < 0 ? -1 : resordsRemain;
            }
        }

        [RelayCommand]
        public async Task SearchMenuItems()
        {
            MenuItems.Clear();
            PageNumber = 1;
            await FetchMenuItems();
        }

        [RelayCommand]
        public async Task ChangePageNumber()
        {
            PageNumber += 1;
            await FetchMenuItems();
        }

        [RelayCommand]
        public async Task ClearSearch(TextChangedEventArgs args)
        {
            if(args.OldTextValue.Length > 0 
                && args.NewTextValue.Length == 0)
            {
                Search = string.Empty;
                MenuItems.Clear();
                await FetchMenuItems();
            }
        }

        [RelayCommand]
        public async Task SortItems(string sortPtoperty)
        {
            IsAscending = OrderBy == sortPtoperty ? true : false;
            OrderBy = sortPtoperty;
            MenuItems.Clear();
            PageNumber = 1;
            await FetchMenuItems();
        }

        [RelayCommand]
        public async Task SelectMenuItem(MenuItem item)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "Menu", _menu },
                { "Category", _category },
                { "ItemId", item.Id }
            };

            await Shell.Current?.GoToAsync(nameof(MenuItemDetailsPage), true, parameters);
        }

        [RelayCommand]
        public async Task GoToMenu()
        {
            await Shell.Current?.GoToAsync($"///{nameof(MenuPage)}");
        }
    }
}
