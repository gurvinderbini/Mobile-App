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
using Xamarin.Forms;

//[assembly: ExportRenderer(typeof(CustomStackLayout), typeof(CustomStackLayoutRenderer))]
namespace App14.Droid
{
    class CustomStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            this.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.shape_rounded_blue_rect));
            //this.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.))
        }
    }
}