using App14.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App14
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Detail2 : ContentPage
    {
        location loc = new location();
        public static double btnLocationX;
        public static double btnLocationY;
        ComClass comfun = new ComClass();
        public int reminders = 0;

        public Detail2()
        {
            try
            {
                InitializeComponent();

                // DisplayAlert("Network Connection", comfun.isConnected().ToString(), "ok");
                AbsoluteLayout.SetLayoutBounds(absMain, new Rectangle(0, 0, location.screenX, location.screenY - 25));
                /*
                btnLocationX = location.btnMenuLocationX;
                btnLocationY = location.btnMenuLocationY;
                AbsoluteLayout.SetLayoutBounds(absMain, new Rectangle(0, 0, location.screenX, location.screenY - 25));
                AbsoluteLayout.SetLayoutBounds(backlayout, new Rectangle(0, 0, location.screenX, location.screenY));
                AbsoluteLayout.SetLayoutBounds(btnlayout, new Rectangle(45, 45, location.screenX , location.screenY));
                AbsoluteLayout.SetLayoutBounds(mainStack, new Rectangle(btnLocationX, btnLocationY, 45, 45));
                
                Menu.ItemTapped += (sender, e) =>
                {
                    var evnt = (SelectedItemChangedEventArgs)e;
                    string text = (string)evnt.SelectedItem;
                    if(text == "menucircle")
                    {
                      //  App.NavigateMasterDetail(new DeviceInfo());
                        backlayout.IsVisible = true;
                        btnlayout.IsVisible = true;
                    }
                    else if(text == "closecircle")
                    {
                        btnlayout.IsVisible = false;
                        backlayout.IsVisible = false;
                    }
                };
                */

                getDashBoardData();

                // on create itemWarnings
                try
                {
                    var tapGestureitemWarnings = new TapGestureRecognizer();
                    tapGestureitemWarnings.Tapped += (s, e) =>
                    {
                        try
                        {
                            App.NavigateMasterDetail(new Warnings());
                        }
                        catch { }
                    };
                    itemWarnings.GestureRecognizers.Add(tapGestureitemWarnings);
                }
                catch { }

                // on create itemOnlineDevices
                try
                {
                    var tapGestureitemOnlineDevices = new TapGestureRecognizer();
                    tapGestureitemOnlineDevices.Tapped += (s, e) =>
                    {
                        try
                        {
                            App.NavigateMasterDetail(new onlineDevices());
                        }
                        catch { }
                    };
                    itemOnlineDevices.GestureRecognizers.Add(tapGestureitemOnlineDevices);
                }
                catch { }

                // on create itemConnectedDevices
                try
                {
                    var tapGestureitemConnectedDevices = new TapGestureRecognizer();
                    tapGestureitemConnectedDevices.Tapped += (s, e) =>
                    {
                        try
                        {
                            App.NavigateMasterDetail(new Rooms());
                        }
                        catch { }
                    };
                    itemConnectedDevices.GestureRecognizers.Add(tapGestureitemConnectedDevices);
                }
                catch { }

                // on create itemSecFrstStak
                try
                {
                    var tapGestureitemSecFrstStak = new TapGestureRecognizer();
                    tapGestureitemSecFrstStak.Tapped += (s, e) =>
                    {
                        try
                        {
                            App.NavigateMasterDetail(new Tickets());
                        }
                        catch { }
                    };
                    SecFrstStak.GestureRecognizers.Add(tapGestureitemSecFrstStak);
                }
                catch { }

                // on calender
                try
                {
                    var tapGesturelblTodolist = new TapGestureRecognizer();
                    tapGesturelblTodolist.Tapped += (s, e) =>
                    {
                        try
                        {
                            App.NavigateMasterDetail(new CalendarView());
                        }
                        catch { }
                    };
                    TODOLIST.GestureRecognizers.Add(tapGesturelblTodolist);
                }
                catch { }
            }
            catch { }

        }


        private async void getDashBoardData()
        {
            // getting timesheet id & timesheet status
            if (comfun.isConnected())
            {
                try
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(App.api_url);
                    var values = new Dictionary<string, string>();
                    values.Add("session_string", App.session_string);
                    var content = new FormUrlEncodedContent(values);
                    HttpResponseMessage response = await client.PostAsync("/itcrm/getDashboardLogs/", content);
                    var result = await response.Content.ReadAsStringAsync();
                    //await DisplayAlert("getting dashboard result", result.ToString(), "OK");
                    statusCheck chk_status = JsonConvert.DeserializeObject<statusCheck>(result);
                    //await DisplayAlert("status!", chk_status.status.ToString(), "ok");
                    if (chk_status.status)
                    {
                        // await DisplayAlert("status!", "true", "ok");
                        Dashboard Dashboard_list = JsonConvert.DeserializeObject<Dashboard>(result);
                        var detail = Dashboard_list.result[0];
                        try
                        {
                            lblNoOfConDevices.Text = detail.devices;
                            lblOnlineDevices.Text = detail.online;
                            lblWarnings.Text = detail.warnings;
                            lblTickets.Text = detail.tickets;
                            //lblTodolist.Text = detail.todo;
                            lblCSFixIt.Text = detail.LCSFI;
                            aiDevices.IsRunning = false;
                        }
                        catch (Exception e)
                        {
                            //await DisplayAlert("Error!", e.Message, "OK");
                        }
                        aiDevices.IsRunning = false;
                    }
                    reminders = await App.Database.noOfEvents();
                    lblReminders.Text = reminders.ToString();
                }
                catch (Exception e)
                {
                    // await DisplayAlert("Error!", e.Message, "OK");
                }

                // await  DisplayAlert("no", App.Database.noOfEvents() + " yes", "ok");
            }
            else
            {
                await DisplayAlert("Connection", "Internet Connection Disabled", "Ok");
            }
        }

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void btnWarning_Clicked(object sender, EventArgs e)
        {
            App.NavigateMasterDetail(new Warnings());
        }

        private void btnTicketsDetail_Clicked(object sender, EventArgs e)
        {
            App.NavigateMasterDetail(new Tickets());
        }

        private void btnRooms_Clicked(object sender, EventArgs e)
        {
            App.NavigateMasterDetail(new Rooms());
        }

        private void btnHelp_Clicked(object sender, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri("http://cloudschool.management/itcrm/helpPortal"));
            }
            catch { }
        }


    }
}