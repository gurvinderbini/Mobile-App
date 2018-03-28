using App14.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App14
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : ContentPage
    {
        public NotificationsPage()
        {
            InitializeComponent();
           
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var list =await App.Database.GetAllNotfications();
            
            NotificationsListview.ItemsSource = list ?? new List<NotificationBO>();
        }
    }
}