using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models.Menu;
using RestaurantAppClient.Pages;
using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.ViewModels
{
    public partial class MenuItemDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        public MenuItem _menuItem;

        private string Menu { get; set; }
        private Guid ItemId { get; set; }
        private string Category { get; set; }

        private readonly IAlertService _alertService;
        private readonly ICartService _cartService;
        private readonly ApiClient _apiClient;

        public MenuItemDetailsViewModel(IAlertService alertService, IClientFactory clientFactory, 
            ICartService cartService)
        {
            _alertService = alertService;
            _cartService = cartService;
            _apiClient = clientFactory.CreateClient();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Menu = query[nameof(Menu)] as string;
            ItemId = (Guid)query[nameof(ItemId)];
            Category = query[nameof(Category)] as string;
        }

        public async Task Initialize()
        {
            var response = Menu switch
            {
                "Drink" => await FetchItemDetails<MenuDrinkItem>(),
                _ => await FetchItemDetails<MenuFoodItem>()
            };

            if(response == null)
            {
                return;
            }

            MenuItem = response;
            IsLoaded = true;
        }

        public async Task<MenuItem> FetchItemDetails<T>()
            where T : MenuItem
        {
            var response = await ExecuteWithLoadingIndicator(async () =>
                await _apiClient.GetMenuItemDetail<T>(Menu, Category, ItemId));

            if (response.Exception != null)
            {
                await _alertService.DisplayResponseAlert(response.Exception, "Ok");
            }

            return response.Data;
        }

        [RelayCommand]
        public async Task GoToMenuItems()
        {
            var data = new Dictionary<string, object>()
            {
                { "Menu", Menu },
                { "Category", Category }
            };
            await Shell.Current?.GoToAsync(nameof(MenuItemsPage), data);
        }

        [RelayCommand]
        public void AddToCart(MenuItem menuItem)
        {
            _cartService.AddItem(menuItem);
        }
    }
}
