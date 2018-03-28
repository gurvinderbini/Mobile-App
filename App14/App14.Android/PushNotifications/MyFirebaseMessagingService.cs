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
                string text = String.Empty;
                base.OnMessageReceived(message);
                Random random = new Random();

                foreach (var item in message.Data)
                {
                    if (item.Key == "Message")
                    {
                        text = item.Value;
                    }
                }

                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop | ActivityFlags.NewTask);

                PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.OneShot);

                var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
                var notificationBuilder = new NotificationCompat.Builder(this)
                    .SetSmallIcon(Resource.Drawable.icon)
                    .SetContentTitle("SSA")
                    .SetContentText(text)
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