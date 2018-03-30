using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Net;
using Android.Content.Res;

namespace App14.Droid
{
    //, ScreenOrientation = ScreenOrientation.Portrait
    [Activity(Label = "App14", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            RequestedOrientation = ScreenOrientation.Portrait;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //allowing the device to change the screen orientation based on the rotation
            Xamarin.Forms.MessagingCenter.Subscribe<WebView>(this, "allowLandScapePortrait", sender => { RequestedOrientation = ScreenOrientation.Landscape; });


            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

        }

        protected override void OnNewIntent(Intent intent)
        {


            if (intent.HasExtra("SomeSpecialKey"))
            {

            }
            base.OnNewIntent(intent);

         
        }

    }
}

