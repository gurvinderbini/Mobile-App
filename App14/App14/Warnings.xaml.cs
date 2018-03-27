using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App14.Models;
using System.ComponentModel;

namespace App14
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Warnings : ContentPage
    {
        public static List<string> list = new List<string>();
        public static List<string> Device_id_list = new List<string>();
        ComClass comfun = new ComClass();
        public static string[] colors = new string[] { "#0378B2", "#9D151C", "#31AD31", "#0174CA" };
        private static int count = 0;
        private static readonly HttpClient client = new HttpClient();
        public static ObservableCollection<SetWarningsList> dt;
        location loc = new location();
        public static double btnLocationX;
        public static double btnLocationY;

        public Warnings()
        {
            InitializeComponent();
            count = 0;
            AbsoluteLayout.SetLayoutBounds(workingStack, new Rectangle(0, 0, location.screenX, location.screenY));
            /*btnLocationX = location.btnMenuLocationX;
            btnLocationY = location.btnMenuLocationY;
            //DisplayAlert("loc", "x: " + btnLocationX + " y: " + btnLocationY,"ok");
            AbsoluteLayout.SetLayoutBounds(workingStack, new Rectangle(0, 0, location.screenX, location.screenY));
            AbsoluteLayout.SetLayoutBounds(backlayout, new Rectangle(0, 0, location.screenX, location.screenY));
            AbsoluteLayout.SetLayoutBounds(btnlayout, new Rectangle(45, 45, location.screenX, location.screenY));
            AbsoluteLayout.SetLayoutBounds(mainStack, new Rectangle(btnLocationX, btnLocationY, 45, 45));

            Menu.ItemTapped += async (sender, e) =>
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
            try
            {
                GetWarnings();
                timer(0);
            }
            catch
            { }
        }

        private async void GetWarnings()
        {
            if (comfun.isConnected())
            {
                try
                {
                    var client = new HttpClient();
                    client.BaseAddress = new Uri(App.api_url);
                    var values = new Dictionary<string, string>();

                    values.Add("session_string", App.session_string);
                    values.Add("data", "{\"table\":\"warnings\",\"fields\":\"id,device_id,modified as date,warnings_count\"}");
                    values.Add("extra", "{\"has_warning\":\"1\"}");
                    values.Add("join", "[{\"table\":\"devices\",\"fields\":\"name,status\",\"where\":\"id\",\"is\":\"warnings.device_id\"}]");
                    var content = new FormUrlEncodedContent(values);
                    HttpResponseMessage response = await client.PostAsync("/itcrm/getElements/", content);

                    var result = await response.Content.ReadAsStringAsync();
                    //await DisplayAlert("result!", result, "ok");
                    try
                    {
                        WarningsResult warnings_list = JsonConvert.DeserializeObject<WarningsResult>(result);
                        var lst = warnings_list.result;
                        dt = new ObservableCollection<SetWarningsList>();
                        lvWarningsList.ItemsSource = dt;

                        /*try
                        {
                            for (int i = 0; i < lst.Length; i++)
                            {
                                //if (lst[i].name == "")
                                //{
                                //   // i = i - 1; 
                                //    //  await DisplayAlert("lst[i].name", "name : " + lst[i].name.ToString(), "ok");
                                //}
                                //else
                                //{
                                   // await DisplayAlert("Detail", "warning id : " + lst[i].id +  " name : " + lst[i].name +  " dev id: " + lst[i].device_id, "ok");
 //                                   list.Add(lst[i].id);
    //                                Device_id_list.Add(lst[i].device_id);
                               // }
                            }
                        }
                        catch (Exception e)
                        {
                          //  await DisplayAlert("E1", e.Message, "ok");
                        }*/
                        try
                        {
                            for (int i = 0; i < lst.Length; i++)
                            {
                                try
                                {
                                    string ionline = "https://cloudschool.management/itcrm/media/images/offline.png";
                                    string ionline_status = "Offline";

                                    bool status = await getDeviceOnlineStatus(lst[i].device_id);
                                    if (status)
                                    {
                                        ionline = "https://cloudschool.management/itcrm/media/images/online.png";
                                        ionline_status = "Online";
                                    }
                                    dt.Add(new SetWarningsList()
                                    {
                                        id = lst[i].id,
                                        device_id = lst[i].device_id,
                                        name = lst[i].name,
                                        date = lst[i].date,
                                        warnings_count = lst[i].warnings_count + " warning(s)",
                                        color = colors[count],
                                        icon = frstWord(lst[i].name), //"https://cloudschool.management/itcrm/media/images/warningBlack.png",
                                        online = ionline,
                                        online_status = ionline_status
                                    });
                                    list.Add(lst[i].id);
                                    Device_id_list.Add(lst[i].device_id);
                                }
                                catch (Exception e)
                                {
                                //    await DisplayAlert("Error2!", e.Message, "ok");
                                }

                                if (count > 3)
                                {
                                    count = 0;
                                }
                                count++;
                            }
                        }
                        catch { }
                        aiDevices.IsRunning = false;
                    }
                    catch { }
                    aiDevices.IsRunning = false;
                }
                catch { }
            }
            else
            {
                await DisplayAlert("Connection", "Internet Connection Disabled", "Ok");
            }
        }

        public string setVal(string val)
        {
            if(string.IsNullOrEmpty(val))
            {
                val = "Null";
            }
            return val;
        }

        public string frstWord(string val)
        {
            char[] a = val.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            string data = a[0].ToString();
            return data;
        }

        private void timer(double time)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () => {
                time += 1;
                TimeSpan t = TimeSpan.FromSeconds(time);
                if (time == 15)
                {
                    udpateDevices();
                    time = 0;
                }

                return true;
            });
        }

        public void udpateDevices()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Updating device", "Devices updated");
                getDataFromService();
            }
            catch { }
        }

        public async void getDataFromService()
        {
            try
            {
                string joined = string.Join(",", Device_id_list);
                var responseString = await client.GetStringAsync("https://remote.cloudschool.management/musaError000.php?device=" + joined);
                string onlineDevices = responseString.ToString();

                string[] a = onlineDevices.Split(',');
                int iii = 0;
                foreach (string s in a)
                {
                    string[] b = s.Split('=');
                    if (b[1] == "true")
                    {
                        dt[iii].online_status = "Online";
                        dt[iii].online = "https://cloudschool.management/itcrm/media/images/online.png";
                    }
                    iii++;
                }
                foreach (SetWarningsList d in dt)
                {
                    System.Diagnostics.Debug.WriteLine("Updating device", d.online_status + " = " + d.name);
                }
            }
            catch { }
        }

        public async Task<bool> getDeviceOnlineStatus(string device_id)
        {
            bool status = false;
            try
            {
                var responseString = await client.GetStringAsync("https://remote.cloudschool.management/musaError000.php?device=" + device_id);
                if (responseString == "true")
                {
                    status = true;
                }
            }
            catch { }
            return status;
        }

        protected override bool OnBackButtonPressed()
        {
            Navigation.PushAsync(new MainPage());
            base.OnBackButtonPressed();
            return true;
        }

        private void lvWarningsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var index = (lvWarningsList.ItemsSource as ObservableCollection<SetWarningsList>).IndexOf(e.SelectedItem as SetWarningsList);
                //DisplayAlert("index", index.ToString() + " " + list[index].ToString(), "ok");
                var a = new SetWarningsList();
                ObservableCollection<SetWarningsList> dt = new ObservableCollection<SetWarningsList>();
                App.NavigateMasterDetail(new WarningDetail(list[index]));
            }
            catch { }
        }

        private async void btnAccess_Clicked(object sender, EventArgs e)
        {            
            try
            {
                var item = (Xamarin.Forms.Button)sender;
                SetWarningsList lstitem = (from items in dt where items.device_id == item.CommandParameter.ToString() select items).FirstOrDefault<SetWarningsList>();
                string device_id = lstitem.device_id.ToString();
                //await DisplayAlert("device_id", device_id, "ok");
                try
                {
                    App.NavigateMasterDetail(new WebView(device_id));
                }
                catch { }

            }
            catch (Exception ex)
            {
               // await DisplayAlert("Error!", ex.Message, "Ok");
            }
        }
    }
    public class SetWarningsList : INotifyPropertyChanged
    {
        string OnlineChanged = "";
        string OnlineStatusChanged = "";
        public event PropertyChangedEventHandler PropertyChanged;

        public string id { get; set; }
        public string device_id { get; set; }
        public string name { get; set; }
        public string device_name { get; set; }
        public string warnings_device_id { get; set; }
        public string antivirus { get; set; }
        public string temperature { get; set; }
        public string hdd_status { get; set; }
        public string icon { get; set; }
        public string warnings_count { get; set; }
        public string date { get; set; }
        public string color { get; set; }
        public string status { get; set; }

        public string online_status
        {
            get
            {
                return OnlineStatusChanged;
            }
            set
            {
                OnlineStatusChanged = value;
                OnPropertyChanged("online_status");
            }
        }

        public string online
        {
            get
            {
                return OnlineChanged;
            }

            set
            {
                OnlineChanged = value;
                OnPropertyChanged("online");
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}