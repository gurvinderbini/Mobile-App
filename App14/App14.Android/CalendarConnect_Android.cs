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
using Android.Provider;
using Android.Database;
using Xamarin.Forms;
using Java.Util;

[assembly: Xamarin.Forms.Dependency(typeof(App14.Droid.CalendarConnect_Android))]
namespace App14.Droid
{
    class CalendarConnect_Android : CalendarConnect
    {
        public string fromAndroidNative(string name)
        {
            string com = "Hello " + name + " I am from Android";
            return com;
        }

        public List<string> CalendarList()
        {
            // List Calendars
            var calendarsUri = CalendarContract.Calendars.ContentUri;

            string[] calendarsProjection = {
               CalendarContract.Calendars.InterfaceConsts.Id,
               CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
               CalendarContract.Calendars.InterfaceConsts.AccountName
            };

            Android.Content.Context context = Android.App.Application.Context;

            var loader = new CursorLoader(context, calendarsUri, calendarsProjection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();

            string[] sourceColumns = {
                CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                CalendarContract.Calendars.InterfaceConsts.AccountName
            };

            ///Kamran
            int counter = 0;
            string a = "";
            List<string> list = new List<string>();
            while (cursor.MoveToNext())
            {
                cursor.MoveToPosition(counter);
                string calId = cursor.GetString(cursor.GetColumnIndex(calendarsProjection[1]));
                list.Add(calId);
                a = calId.ToString() + " , " + a;
                counter++;
            }
            //// Kamran
            return list;
        }

        public string AddEvent(string title, DateTime startDate, string shour, string smin, DateTime endDate, string ehour, string emin)
        {
            string smonth = startDate.ToString("MM");
            string sDate = startDate.ToString("dd");
            string syear = startDate.ToString("yyyy");
            
            int sy = Convert.ToInt16(syear);
            int sm = Convert.ToInt16(smonth);
            int sd = Convert.ToInt16(sDate);
            int sho = Convert.ToInt16(shour);
            int sminutes = Convert.ToInt16(smin);

            string emonth = endDate.ToString("MM");
            string eDate = endDate.ToString("dd");
            string eyear = endDate.ToString("yyyy");
            
            int ey = Convert.ToInt16(eyear);
            int em = Convert.ToInt16(emonth);
            int ed = Convert.ToInt16(eDate);
            int eho = Convert.ToInt16(ehour);
            int eminutes = Convert.ToInt16(emin);
            
            try
            {
                ContentValues eventValues = new ContentValues();
                eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, 1);
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "This is an event created from Mono for Android by MK");
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(sy, sm, sd, sho, sminutes));
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(ey, em, ed, eho, eminutes));

                // GitHub issue #9 : Event start and end times need timezone support.
                // https://github.com/xamarin/monodroid-samples/issues/9
                eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
                eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

                var uri = Forms.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
                return "Uri for new event: " + uri;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public long GetDateTimeMS(int year, int month, int day, int hr, int min)
        {
            Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Java.Util.CalendarField.Year, year);
            c.Set(Java.Util.CalendarField.Month, month-1);
            c.Set(Java.Util.CalendarField.DayOfMonth, day);
            c.Set(Java.Util.CalendarField.HourOfDay, hr);
            c.Set(Java.Util.CalendarField.Minute, min);
            return c.TimeInMillis;
        }
    }
}