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
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using static Android.OS.DropBoxManager;
using Xamarin.Forms;
using App14;
using App14.Droid.Custom;

[assembly: ExportRenderer(typeof(CustomizedEntry), typeof(CustomEntry))]
namespace App14.Droid.Custom
{
    class CustomEntry : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                //Control.SetBackgroundResource(Resource.Layout.rounded_shape);
                var gradientDrawable = new GradientDrawable();
                //gradientDrawable.SetCornerRadius(60f);
                //gradientDrawable.SetStroke(2, Android.Graphics.Color.DeepPink);
                gradientDrawable.SetColor(Android.Graphics.Color.LightGray);
                //gradientDrawable.
                Control.SetBackground(gradientDrawable);

                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight,
                    Control.PaddingBottom);
            }
        }
    }
}