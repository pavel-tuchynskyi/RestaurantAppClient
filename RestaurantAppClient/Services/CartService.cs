using RestaurantAppClient.Common.Interfaces;
using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.Services
{
    public class CartService : ICartService
    {
        private List<MenuItem> _menuItems = new();

        public void AddItem(MenuItem item)
        {
            _menuItems.Add(item);
        }

        public void RemoveItem(MenuItem item)
        {
            _menuItems.Remove(item);
        }

        public MenuItem GetItem(int index)
        {
            return _menuItems[index];
        }

        public List<MenuItem> GetItems()
        {
            return _menuItems;
        }

        public void Clear()
        {
            _menuItems.Clear();
        }
    }
}
