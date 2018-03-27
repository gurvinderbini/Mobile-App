using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamForms.Controls;

namespace App14
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarView : ContentPage
    {
        location loc = new location();
        public static double btnLocationX;
        public static double btnLocationY;

        public CalendarView()
        {
            InitializeComponent();
            AbsoluteLayout.SetLayoutBounds(absMain, new Rectangle(0, 0, location.screenX, location.screenY - 25));
            
            /*btnLocationX = location.btnMenuLocationX;
            btnLocationY = location.btnMenuLocationY;
            AbsoluteLayout.SetLayoutBounds(absMain, new Rectangle(0, 0, location.screenX, location.screenY - 25));
            AbsoluteLayout.SetLayoutBounds(backlayout, new Rectangle(0, 0, location.screenX, location.screenY));
            AbsoluteLayout.SetLayoutBounds(btnlayout, new Rectangle(45, 45, location.screenX, location.screenY));
            AbsoluteLayout.SetLayoutBounds(mainStack, new Rectangle(btnLocationX, btnLocationY, 45, 45));

            Menu.ItemTapped += (sender, e) =>
            {
                var evnt = (SelectedItemChangedEventArgs)e;
                string text = (string)evnt.SelectedItem;
                if (text == "menucircle")
                {
                    backlayout.IsVisible = true;
                    btnlayout.IsVisible = true;
                }
                else if (text == "closecircle")
                {
                    btnlayout.IsVisible = false;
                    backlayout.IsVisible = false;
                }
            };
            */

            calendar.SelectedTextColor = Color.Yellow;
            calendar.CalendarStartDate(DateTime.Now);
            calendar.BorderWidth = 1;
            calendar.DatesBackgroundColor = Color.White;

            // title bar weekdays
            calendar.WeekdaysTextColor = Color.Black;
            calendar.WeekdaysBackgroundColor = Color.White;

            calendar.TitleLeftArrowTextColor = Color.Green;
            calendar.TitleRightArrowTextColor = Color.Green;

            calendar.NumberOfWeekTextColor = Color.Green;

            calendar.SelectedTextColor = Color.Green;
            calendar.SelectedBorderColor = Color.Green;
            calendar.SelectedBorderWidth = 2;


            calendar.DateClicked += (object sender, DateTimeEventArgs e) =>
            {
                var dateSelect = calendar.SelectedDate;
                DateTime da = Convert.ToDateTime(dateSelect);
              //  App.NavigateMasterDetail(new TodayReminderList(da.ToString("dd-MM-yyyy")));
              //  lbl.Text = "Date selected: " + da.ToString("dd-MM-yyyy");
            };
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new MainPage());
            base.OnBackButtonPressed();
            return true;
        }

        private void addReminder_Clicked(object sender, EventArgs e)
        {
            try
            {
                App.NavigateMasterDetail(new addSchedule());
            }
            catch { }
        }
    }
}