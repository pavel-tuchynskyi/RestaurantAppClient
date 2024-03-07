using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models;
using RestaurantAppClient.Models.Orders;
using System.Collections.ObjectModel;

namespace RestaurantAppClient.ViewModels
{
    public partial class OrdersViewModel : BaseViewModel
    {
        public ObservableCollection<Order> Orders { get; set; } = new();

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

        private readonly ApiClient _apiClient;
        private readonly IAlertService _alertService;

        public OrdersViewModel(IClientFactory clientFactory, IAlertService alertService)
        {
            _apiClient = clientFactory.CreateClient();
            _alertService = alertService;
        }

        public async Task Initialize()
        {
            if (IsLoaded)
            {
                return;
            }

            await FetchOrders();
            IsLoaded = true;
        }

        public async Task FetchOrders()
        {
            var orderBy = new OrderBy(OrderBy, IsAscending);
            var paging = new Paging(PageNumber, PageSize);

            var response = await ExecuteWithLoadingIndicator(async () =>
                await _apiClient.GetUserOrders(orderBy, paging));

            if (response.Exception != null)
            {
                await _alertService.DisplayResponseAlert(response.Exception, "Ok");
            }
            else
            {
                foreach (var item in response.Data.Items)
                {
                    Orders.Add(item);
                }

                TotalPages = response.Data.TotalPages;
                var resordsRemain = response.Data.TotalRecords - (PageNumber * PageSize);
                RecordsRemain = resordsRemain < 0 ? -1 : resordsRemain;
            }
        }

        [RelayCommand]
        public async Task ChangePageNumber()
        {
            PageNumber += 1;
            await FetchOrders();
        }

        [RelayCommand]
        public async Task SortItems(string sortPtoperty)
        {
            IsAscending = OrderBy == sortPtoperty ? true : false;
            OrderBy = sortPtoperty;
            Orders.Clear();
            PageNumber = 1;
            await FetchOrders();
        }
    }
}
