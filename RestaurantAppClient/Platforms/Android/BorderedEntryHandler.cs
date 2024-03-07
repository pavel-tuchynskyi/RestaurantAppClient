using Android.Graphics.Drawables;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Controls.Platform;
using RestaurantAppClient.Controls;

namespace RestaurantAppClient.Platforms.Android
{
    public static class BorderedEntryHandler
    {
        public static void Map(IElementHandler handler, IElement view)
        {
            if (view is BorderedEntry borderedEntry)
            {
                var entryHandler = (EntryHandler)handler;
                
                var gd = new GradientDrawable();

                gd.SetCornerRadius((int)handler.MauiContext?.Context.ToPixels(borderedEntry.BorderRadius));

                gd.SetStroke((int)handler.MauiContext?.Context.ToPixels(borderedEntry.BorderThickness), borderedEntry.BorderColor.ToAndroid());

                if (borderedEntry.BackgroundColor != null)
                {
                    gd.SetColor(borderedEntry.BackgroundColor.ToAndroid());
                }

                entryHandler.PlatformView?.SetBackground(gd);

                var paddingLeft = (int)handler.MauiContext?.Context.ToPixels(borderedEntry.BorderPadding.Left);
                var paddingTop = (int)handler.MauiContext?.Context.ToPixels(borderedEntry.BorderPadding.Top);
                var paddingRight = (int)handler.MauiContext?.Context.ToPixels(borderedEntry.BorderPadding.Right);
                var paddingBottom = (int)handler.MauiContext?.Context.ToPixels(borderedEntry.BorderPadding.Bottom);

                entryHandler.PlatformView?.SetPadding(paddingLeft, paddingTop, paddingRight, paddingBottom);
            }
        }
    }
}
