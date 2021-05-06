using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class ListadoProductosView : ContentPage
    {



		public ListadoProductosView()
        {
            InitializeComponent();
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            NavigationPage.SetBackButtonTitle(this, "Volver");

            

            BindingContext = new ListadoProductosViewModel();
                     
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }
    }
}