using System.Collections;
using System.Globalization;

namespace RestaurantAppClient.Common.Converters
{
    public class CollectionLengthToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value.GetType().IsAssignableTo(typeof(IList)))
            {
                return ((IList)value).Count > 0 ? true : false;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
