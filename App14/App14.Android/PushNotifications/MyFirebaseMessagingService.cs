using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using App14.Models;
using Firebase.Messaging;

namespace App14.Droid.PushNotifications
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        const string TAG = "MyFirebaseMsgService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                NotificationBO notificationBo = new NotificationBO();

                base.OnMessageReceived(message);
                Random random = new Random();


                foreach (var item in message.Data)
                {
                    if (item.Key == "title")
                    {
                        notificationBo.Title = item.Value;
                    }
                    if (item.Key == "message")
                    {
                        notificationBo.Message = item.Value;
                    }
                    if (item.Key == "screen")
                    {
                        notificationBo.Screen = item.Value;
                    }
                    if (item.Key == "body")
                    {
                        notificationBo.Body = item.Value;
                    }
                    if (item.Key == "sound")
                    {
                        notificationBo.Sound = item.Value;
                    }
                    if (item.Key == "content_available")
                    {
                        notificationBo.ContentAvailable = item.Value;
                    }
                }

                App.Database.InsertNotification(notificationBo);


                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop | ActivityFlags.NewTask);
                intent.PutExtra("notification", "notification");
                PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.OneShot);

                var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                var notificationBuilder = new NotificationCompat.Builder(this)
                    .SetSmallIcon(Resource.Drawable.icon)
                    .SetContentTitle(notificationBo.Title)
                    .SetContentText(notificationBo.Message)
                    .SetAutoCancel(true)
                    .SetSound(defaultSoundUri)
                    .SetContentIntent(pendingIntent);
                var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                notificationManager.Notify(random.Next(100000), notificationBuilder.Build());
            }
            catch (Exception e)
            {
            }
        }
    }
}