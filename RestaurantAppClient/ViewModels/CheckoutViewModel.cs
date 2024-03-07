using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models.Orders;
using RestaurantAppClient.Pages;
using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.ViewModels
{
    public partial class CheckoutViewModel : BaseViewModel
    {
        private readonly IAlertService _alertService;
        private readonly ICartService _cartService;
        private readonly ILocationService _locationService;
        private readonly ApiClient _apiClient;
        private List<MenuItem> _items;

        [ObservableProperty]
        private Location _orderLocation;

        [ObservableProperty]
        private string _city;

        [ObservableProperty]
        private string _address;

        [ObservableProperty]
        private string _apartments;

        public CheckoutViewModel(IAlertService alertService, 
            ICartService cartService, 
            IClientFactory clientFactory,
            ILocationService locationService)
        {
            _alertService = alertService;
            _cartService = cartService;
            _locationService = locationService;
            _apiClient = clientFactory.CreateClient();
        }

        public async Task Initialize()
        {
            _items = _cartService.GetItems();

            OrderLocation = await _locationService.GetLocationAsync();
            await SetDeliveryAddress(OrderLocation);
        }

        public async Task SetDeliveryAddress(Location location)
        {
            var locationPlacemark = await _locationService.GetLocationPlacemark(location);
            GetOrderAddress(locationPlacemark);
        }

        private void GetOrderAddress(Placemark placemark)
        {
            City = placemark.Locality;
            Address = string.Format("{0}, {1}", placemark.Thoroughfare, placemark.SubThoroughfare);
        }

        public async Task SetDeliveryLocation()
        {
            if(string.IsNullOrWhiteSpace(City) || string.IsNullOrWhiteSpace(Address))
            {
                return;
            }

            var location = await _locationService.GetAddressLocation(City, Address);

            if (location != null)
            {
                OrderLocation = location;
            }
        }

        [RelayCommand]
        public async Task CreateOrder()
        {
            if (string.IsNullOrWhiteSpace(City) 
                || string.IsNullOrWhiteSpace(Address))
            {
                await _alertService.DisplayAlertMessage("Please choose delivery location");
            }

            var order = new OrderCreate()
            {
                City = City,
                Address = string.Format("{0}, {1}", Address, Apartments),
                Items = _items.Select(x => x.Id).ToList()
            };

            var response = await ExecuteWithLoadingIndicator(async () => await _apiClient.CreateOrder(order));

            if (response.Exception != null)
            {
                await _alertService.DisplayResponseAlert(response.Exception, "Ok");
            }
            else
            {
                _cartService.Clear();
                await Shell.Current?.GoToAsync($"///{nameof(MenuPage)}", true);
            }
        }
    }
}
