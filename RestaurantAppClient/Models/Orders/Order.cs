namespace RestaurantAppClient.Models.Orders
{
    public class Order
    {
        public string City { get; set; }
        public string Address { get; set; }
        public List<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Price { get; set; }
    }
}
