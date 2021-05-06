using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class NuevaParcelaPaso2View : ContentPage
    {
      
        public NuevaParcelaPaso2View()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
          
            InitializeComponent();

            BindingContext = new NuevaParcelaPaso2ViewModel();
            NavigationPage.SetBackButtonTitle(this, "Volver");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }


    }
}