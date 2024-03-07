namespace RestaurantAppClient.Models.Orders
{
    public record OrderGetAll (Guid UserId, OrderBy OrderBy, Paging Paging);
}
