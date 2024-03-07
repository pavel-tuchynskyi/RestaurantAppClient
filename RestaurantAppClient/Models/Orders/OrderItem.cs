using RestaurantAppClient.Models.Shared;

namespace RestaurantAppClient.Models.Orders
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemImage Image { get; set; }
        public decimal Price { get; set; }
    }
}
