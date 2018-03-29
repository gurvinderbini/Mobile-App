using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Iid;

namespace App14.Droid.PushNotifications
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseServiceId : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override async void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            App.DeviceToken = refreshedToken;
          // await App.Database.InsertToken(new Models.DeviceTokenBO() {DeviceToken = App.DeviceToken});
            //Settings.SSA_IosDeviceToken = refreshedToken;
            // Settings.SSA_IosDeviceToken = refreshedToken;
        }
    }
}