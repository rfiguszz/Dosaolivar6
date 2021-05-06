using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class RegistroUsuarioView : ContentPage
    {
      
        public RegistroUsuarioView()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
          
            NavigationPage.SetBackButtonTitle(this, "Volver");

            InitializeComponent();
            BindingContext = new RegistroUsuarioViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }
    }
}