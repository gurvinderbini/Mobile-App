using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using App14.Droid.Custom;
using Xamarin.Forms;
using App14;

[assembly: ExportRenderer(typeof(MyCustomContentPage), typeof(CustomContentPageRenderer))]
namespace App14.Droid.Custom
{
    public class CustomContentPageRenderer : Xamarin.Forms.Platform.Android.PageRenderer
    {
        private ScreenOrientation _previousOrientation = ScreenOrientation.Unspecified;


        protected override void OnWindowVisibilityChanged([GeneratedEnum] ViewStates visibility)
        {
            base.OnWindowVisibilityChanged(visibility);
            var activity = (Activity)Context;
            if (visibility == ViewStates.Gone)
            {
                // Revert to previous orientation
                activity.RequestedOrientation = _previousOrientation == ScreenOrientation.Unspecified ?
               ScreenOrientation.Portrait : _previousOrientation;
            }
            else if (visibility == ViewStates.Visible)
            {
                if (_previousOrientation == ScreenOrientation.Unspecified)
                {
                    _previousOrientation = activity.RequestedOrientation;
                }

                activity.RequestedOrientation = ScreenOrientation.Landscape;
            }
        }

    }
}