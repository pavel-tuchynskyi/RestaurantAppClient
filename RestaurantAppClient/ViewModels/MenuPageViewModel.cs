using CommunityToolkit.Mvvm.Input;
using RestaurantAppClient.Models.Menu;
using RestaurantAppClient.Pages;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace RestaurantAppClient.ViewModels
{
    public partial class MenuPageViewModel : BaseViewModel
    {
        public ObservableCollection<Menu> Menus { get; set; } = new();

        public async Task Initialize()
        {
            if(IsLoaded)
            {
                return;
            }

            var menus = await GetMenus();

            foreach (var item in menus)
            {
                Menus.Add(item);
            }

            IsLoaded = true;
        }

        private async Task<List<Menu>> GetMenus()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("menus.json");
            var menus = await JsonSerializer.DeserializeAsync<List<Menu>>(stream);

            return menus;
        }

        [RelayCommand]
        public async Task MenuCategorySelected((string Menu, string Category) data)
        {
            var parameters = new Dictionary<string, object>()
            {
                { "Menu", data.Menu },
                { "Category", data.Category }
            };
            await Shell.Current?.GoToAsync(nameof(MenuItemsPage), parameters);
        }
    }
}
