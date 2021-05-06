using SmartHotel.Clients.Core.Helpers;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class BookingCalendarView : ContentPage
	{
		public BookingCalendarView ()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }

            NavigationPage.SetBackButtonTitle(this, "Volver");

            InitializeComponent ();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }
    }
}