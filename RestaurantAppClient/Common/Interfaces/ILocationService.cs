namespace RestaurantAppClient.Common.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationAsync();
        Task<Placemark> GetLocationPlacemark(Location location);
        Task<Location> GetAddressLocation(string city, string address);
    }
}
