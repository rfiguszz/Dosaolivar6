using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class ListadoBoquillasMarcaView : ContentPage
    {



		public ListadoBoquillasMarcaView()
        {
            InitializeComponent();
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            NavigationPage.SetBackButtonTitle(this, "Volver");

            

            BindingContext = new ListadoBoquillasMarcaViewModel();
                     
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }
    }
}