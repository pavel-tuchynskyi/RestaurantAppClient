using Microsoft.Maui.Handlers;

namespace RestaurantAppClient.Handlers.SearchBar
{
    public partial class SearchBarExHandler : SearchBarHandler
    {
        public static readonly IPropertyMapper<ISearchBar, SearchBarHandler> CustomMapper =
        new PropertyMapper<ISearchBar, SearchBarHandler>(Mapper)
        {
            ["IconColor"] = MapIconColor,
        };

        public SearchBarExHandler() : base(CustomMapper, CommandMapper)
        {
        }

        public override void UpdateValue(string propertyName)
        {
            base.UpdateValue(propertyName);

            if (propertyName == Microsoft.Maui.Controls.SearchBar.TextColorProperty.PropertyName)
            {
                SetIconColor(GetTextColor());
            }
        }
    }
}
