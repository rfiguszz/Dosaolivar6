using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class CalendarioTratamientosView : ContentPage
	{
		public CalendarioTratamientosView()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Volver");
            BindingContext = new CalendarioTratamientosViewModel();


        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            // Only this seems to remove the bindings created from XAML from the ViewModel
            this.BindingContext = null; // <--- YAY!!! Bindings now removed

            base.OnDisappearing();

            // Only this seems to remove the bindings created from XAML from the ViewModel
            this.BindingContext = null; // <--- YAY!!! Bindings now removed

        }
    }
}