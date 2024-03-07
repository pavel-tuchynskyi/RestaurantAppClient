using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Popups;

namespace RestaurantAppClient.Services
{
    public class LocationService : ILocationService
    {
        private readonly IPopupService _popupService;
        private readonly IAlertService _alertService;
        private readonly IGeolocation _geolocation;

        public LocationService(IPopupService popupService, IAlertService alertService, IGeolocation geolocation)
        {
            _popupService = popupService;
            _alertService = alertService;
            _geolocation = geolocation;
        }

        private async Task<PermissionStatus> RequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
            {
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                await _popupService.ShowPopup(new InformationPopup("We need access to your location"));
            }

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }

        public async Task<Location> GetLocationAsync()
        {
            return await MainThread.InvokeOnMainThreadAsync(async () =>
            {
                var status = await RequestLocationPermission();

                if (status != PermissionStatus.Granted)
                {
                    await _alertService.DisplayAlertMessage("Don't have permission to get location");
                    return null;
                }

                return await _geolocation.GetLocationAsync();
            });
        }

        public async Task<Placemark> GetLocationPlacemark(Location location)
        {
            if(location == null)
            {
                return null;
            }

            var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();

            return placemark;
        }

        public async Task<Location> GetAddressLocation(string city, string address)
        {
            if(string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(city))
            {
                return null;
            }

            var locations = await Geocoding.Default.GetLocationsAsync(string.Format("{0}, {1}", city, address));
            var location = locations.FirstOrDefault();

            return location;
        }
    }
}
