using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class ListaTratamientosView : ContentPage
    {
        public ListaTratamientosView()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            // NavigationPage.SetBackButtonTitle(this, string.Empty);
            NavigationPage.SetBackButtonTitle(this, "Volver");
            
            InitializeComponent();
            BindingContext = new ListaTratamientosViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }
    }
}