using RestaurantAppClient.Models.Shared;

namespace RestaurantAppClient.Models.Menu
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemImage Image { get; set; }
        public decimal Price { get; set; }
    }
}
