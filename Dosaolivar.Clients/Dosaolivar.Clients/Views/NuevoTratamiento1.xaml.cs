using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class NuevoTratamiento1View : ContentPage
    {
      
        public NuevoTratamiento1View()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
          
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Volver");
            BindingContext = new NuevoTratamiento1ViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }


    }
}