using App14.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App14.SQLite
{
    public class BaseDB
    {
       // public static SQLiteConnection Connection = DependencyService.Get<ISQLite>().GetConnection();

        static BaseDB()
        {
            CreateTables();

        }



        public static void CreateTables()
        {
           // Connection.CreateTable<NotificationBO>();
        }
    }
}
