using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Pages;
using System.Collections.ObjectModel;
using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.ViewModels
{
    public partial class CartPageViewModel : BaseViewModel
    {
        private readonly ICartService _cartService;

        [ObservableProperty]
        private decimal _totalPrice;

        public ObservableCollection<MenuItem> MenuItems { get; set; } = new();

        public CartPageViewModel(ICartService cartService)
        {
            _cartService = cartService;
        }

        public void Initialize()
        {
            MenuItems.Clear();
            TotalPrice = 0;

            var items = _cartService.GetItems();

            foreach (var item in items)
            {
                MenuItems.Add(item);
                TotalPrice += item.Price;
            }
        }

        [RelayCommand]
        public void RemoveItem(MenuItem item)
        {
            MenuItems.Remove(item);
            _cartService.RemoveItem(item);
            TotalPrice -= item.Price;
        }

        [RelayCommand]
        public async Task CreateOrder(MenuItem item)
        {
            if(MenuItems.Count > 0)
            {
                await Shell.Current?.GoToAsync(nameof(CheckoutPage), true);
            }
        }
    }
}
