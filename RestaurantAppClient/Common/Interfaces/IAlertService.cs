using RestaurantAppClient.Models;

namespace RestaurantAppClient.Common.Interfaces
{
    public interface IAlertService
    {
        Task DisplayResponseAlert(ResponseException exception, string cancel);
        Task DisplayAlertMessage(string message);
    }
}
