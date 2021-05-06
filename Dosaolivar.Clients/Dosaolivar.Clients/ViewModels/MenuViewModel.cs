using MvvmHelpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Authentication;
using SmartHotel.Clients.Core.Services.OpenUri;
using SmartHotel.Clients.Core.ViewModels.Base;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class MenuViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        ObservableRangeCollection<Models.MenuItem> menuItems;

        readonly IAuthenticationService authenticationService;
        readonly IOpenUriService openUrlService;

        public MenuViewModel(
            IAuthenticationService authenticationService,
            IOpenUriService openUrlService)
        {
            this.authenticationService = authenticationService;
            this.openUrlService = openUrlService;

            MenuItems = new ObservableRangeCollection<Models.MenuItem>();

            InitMenuItems();
        }

        public string UserName => AppSettings.sessionNick;

        public string UserAvatar => AppSettings.User?.profile_image;

        public ObservableRangeCollection<Models.MenuItem> MenuItems
        {
            get => menuItems;
            set
            {
                menuItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand MenuItemSelectedCommand => new Command<Models.MenuItem>(OnSelectMenuItem);

        public Task OnViewAppearingAsync(VisualElement view)
        {

            return Task.FromResult(true);
        }

        public Task OnViewDisappearingAsync(VisualElement view) => Task.FromResult(true);

        void InitMenuItems()
        {
            MenuItems.Add(new Models.MenuItem
            {
                Title = "Inicio",
                MenuItemType = MenuItemType.Home,
                ViewModelType = typeof(MainViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Mis parcelas",
                MenuItemType = MenuItemType.MisParcelas,
                ViewModelType = typeof(MisParcelasViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Mis equipos",
                MenuItemType = MenuItemType.MisEquipos,
                ViewModelType = typeof(ListadoEquiposViewModel),
                IsEnabled = true
            });

            MenuItems.Add(new Models.MenuItem
            {
                Title = "Mis tratamientos",
                MenuItemType = MenuItemType.MisTratamientos,
                ViewModelType = typeof(ListadoBoquillasMarcaViewModel),
                IsEnabled = true
            });

          
            MenuItems.Add(new Models.MenuItem
            {
                Title = "Salir",
                MenuItemType = MenuItemType.Logout,
                ViewModelType = typeof(LoginViewModel),
                IsEnabled = true,
                AfterNavigationAction = RemoveUserCredentials
            });
        }

        async void OnSelectMenuItem(Models.MenuItem item)
        {


         /*   if (item.MenuItemType == MenuItemType.MisEquipos)
            {

               /* if (Device.RuntimePlatform == Device.UWP)
                {
                    openUrlService.OpenSkypeBot(AppSettings.SkypeBotId);
                }
                else
                {
                    await OpenBotAsync();
                }
            }*/
            if (item.IsEnabled && item.ViewModelType != null)
            {
                item.AfterNavigationAction?.Invoke();
                await NavigationService.NavigateToAsync(item.ViewModelType, item);
            }
        }

        Task RemoveUserCredentials()
        {
            AppSettings.HasBooking = false;

            MessagingCenter.Send(this, MessengerKeys.CheckoutRequested);

            return authenticationService.LogoutAsync();
        }

      /*  void OnBookingRequested(Booking booking)
        {
            if (booking == null)
            {
                return;
            }

            SetMenuItemStatus(MenuItemType.MisParcelas, true);
        }*/

        void OnCheckoutRequested(object args) => SetMenuItemStatus(MenuItemType.MisEquipos, false);

        void SetMenuItemStatus(MenuItemType type, bool enabled)
        {
            var menuItem = MenuItems.FirstOrDefault(m => m.MenuItemType == type);

            if (menuItem != null)
            {
                menuItem.IsEnabled = enabled;
            }
        }

      
    }
}