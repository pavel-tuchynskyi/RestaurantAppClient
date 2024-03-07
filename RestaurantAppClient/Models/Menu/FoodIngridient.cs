using RestaurantAppClient.Models.Shared;

namespace RestaurantAppClient.Models.Menu
{
    public class FoodIngridient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemImage Image { get; set; }
    }
}
