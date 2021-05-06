using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels.Base;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "Volver");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
            NavigationPage.SetBackButtonTitle(this, "Volver");
            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }
    }
}