using RestaurantAppClient.Common.Enums;

namespace RestaurantAppClient.Models.Shared
{
    public class ItemImage
    {
        public byte[] ImageBlob { get; set; }
        public ImageTypes ImageType { get; set; }
    }
}
