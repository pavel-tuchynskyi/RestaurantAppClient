using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.Common.Interfaces
{
    public interface ICartService
    {
        void AddItem(MenuItem item);
        void RemoveItem(MenuItem item);
        MenuItem GetItem(int index);
        List<MenuItem> GetItems();
        void Clear();
    }
}
