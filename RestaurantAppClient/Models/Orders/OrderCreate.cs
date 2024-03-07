namespace RestaurantAppClient.Models.Orders
{
    public class OrderCreate
    {
        public string City { get; set; }
        public string Address { get; set; }
        public List<Guid> Items { get; set; }
    }
}
