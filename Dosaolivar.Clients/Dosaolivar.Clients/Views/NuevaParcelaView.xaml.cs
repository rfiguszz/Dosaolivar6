using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class NuevaParcelaView : ContentPage
    {
      
        public NuevaParcelaView()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
          
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Volver");
            BindingContext = new NuevaParcelaViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }


    }
}