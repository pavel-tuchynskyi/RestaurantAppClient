using CommunityToolkit.Maui.Converters;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace RestaurantAppClient.Common.Converters
{
    public class ByteArrayToImageSourceConverter : BaseConverter<byte[]?, ImageSource?>
    {
        public override ImageSource? DefaultConvertReturnValue { get; set; } = null;

        public override byte[]? DefaultConvertBackReturnValue { get; set; } = null;

        [return: NotNullIfNotNull(nameof(value))]
        public override ImageSource? ConvertFrom(byte[]? value, CultureInfo? culture = null)
        {
            if (value is null)
            {
                return null;
            }

            return ImageSource.FromStream((token) => Task.FromResult((Stream)new MemoryStream(value)));
        }

        public override byte[]? ConvertBackTo(ImageSource? value, CultureInfo? culture = null)
        {
            if (value is null)
            {
                return null;
            }

            if (value is not StreamImageSource streamImageSource)
            {
                throw new ArgumentException("Expected value to be of type StreamImageSource.", nameof(value));
            }

            var streamFromImageSource = streamImageSource.Stream(CancellationToken.None).GetAwaiter().GetResult();

            if (streamFromImageSource is null)
            {
                return null;
            }

            using var memoryStream = new MemoryStream();
            streamFromImageSource.CopyTo(memoryStream);

            return memoryStream.ToArray();
        }
    }
}
