using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels;
using SmartHotel.Clients.Core.ViewModels.Base;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class NuevoEquipoView : ContentPage
    {
      
        public NuevoEquipoView()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
          
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Volver");
            BindingContext = new NuevoEquipoViewModel();
        }

    }
}