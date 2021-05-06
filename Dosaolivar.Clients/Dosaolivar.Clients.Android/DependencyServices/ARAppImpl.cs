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

using SmartHotel.Clients.Droid;
using SmartHotel.Clients.Droid.AR;
using SmartHotel.Clients.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(Dosaolivar.Clients.Droid.DependencyServices.ARAppImpl))]
namespace Dosaolivar.Clients.Droid.DependencyServices
{
    public class ARAppImpl : IARApp
    {
        public void LaunchARMetodo1()
        {
            throw new NotImplementedException();
        }

        public void LaunchARMetodo2()
        {
            var intent = new Intent(MainActivity.Instance, typeof(ARViewActivity));
            //intent.SetFlags(ActivityFlags.NewTask);
            MainActivity.Instance.StartActivity(intent);
        }
    }
}