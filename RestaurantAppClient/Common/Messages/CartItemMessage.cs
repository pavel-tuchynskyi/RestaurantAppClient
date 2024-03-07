using CommunityToolkit.Mvvm.Messaging.Messages;
using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.Common.Messages
{
    public class CartItemMessage : ValueChangedMessage<MenuItem>
    {
        public CartItemMessage(MenuItem value) : base(value)
        {
        }
    }
}
