using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartHotel.Clients.Interfaces;
using SmartHotel.Clients.iOS.AR;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(SmartHotel.Clients.iOS.DependencyServices.ARAppImpl))]
namespace SmartHotel.Clients.iOS.DependencyServices
{
    public class ARAppImpl : IARApp
    {
        public void LaunchARMetodo1()
        {
            ARMetodo1ViewController viewController = new ARMetodo1ViewController();
            UIApplication.SharedApplication.KeyWindow.RootViewController.
              PresentViewController(viewController, true, null);
        }

        public void LaunchARMetodo2()
        {
            ARMetodo2ViewController viewController = new ARMetodo2ViewController();
            //viewController.ModalPresentationStyle = UIModalPresentationStyle.FullScreen;
            UIApplication.SharedApplication.KeyWindow.RootViewController.
              PresentViewController(viewController, true, null);
        }
    }
}