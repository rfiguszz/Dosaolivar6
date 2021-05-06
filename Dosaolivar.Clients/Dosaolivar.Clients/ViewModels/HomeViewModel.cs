using MvvmHelpers;
using SmartHotel.Clients.Core.Exceptions;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Authentication;
using SmartHotel.Clients.Core.Services.Chart;
using SmartHotel.Clients.Core.Services.File;
using SmartHotel.Clients.Core.Services.Notification;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Microcharts;
using SkiaSharp;
using SmartHotel.Clients.Core.Controls;
using SmartHotel.Clients.Core.Services.IoT;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using SmartHotel.Clients.Core.Services.MisParcelas;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class HomeViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        // DOSAOLIVAR
        readonly IParcelasService parcelasService;


        // BORRAR
        bool hasBooking;
        Chart temperatureChart;
        Chart lightChart;
        Chart greenChart;
        ObservableRangeCollection<Notification> notifications;

        readonly INotificationService notificationService;
        readonly IChartService chartService;
        //readonly IBookingService bookingService;
        readonly IAuthenticationService authenticationService;
        readonly IFileService fileService;
		//readonly IRoomDevicesDataService roomDevicesDataService;

        public HomeViewModel(
            INotificationService notificationService,
            IChartService chartService,
            IAuthenticationService authenticationService,
            IFileService fileService,
             IParcelasService parcelasService,
            IRoomDevicesDataService roomDevicesDataService)
        {

            //DOSAOLIVAR
            this.parcelasService = parcelasService;

            // borrar
            this.notificationService = notificationService;
            this.chartService = chartService;
            this.authenticationService = authenticationService;
            this.fileService = fileService;
            notifications = new ObservableRangeCollection<Notification>();
        }



        //-------   INTERFACES

        // Equipo a insertar
     /*   EquipoModel equipo;
        public EquipoModel Equipo
        {
            get => equipo;
            set => SetProperty(ref equipo, value);
        } */



        public bool HasBooking
        {
            get => hasBooking;
            set => SetProperty(ref hasBooking, value);
        }

        public Microcharts.Chart TemperatureChart
        {
            get => temperatureChart;
            set => SetProperty(ref temperatureChart, value);
        }

        public Microcharts.Chart LightChart
        {
            get => lightChart;
			set => SetProperty(ref lightChart, value);
        }

        public Microcharts.Chart GreenChart
        {
            get => greenChart;
            set => SetProperty(ref greenChart, value);
        }

        public ObservableRangeCollection<Notification> Notifications
        {
            get => notifications;
            set => SetProperty(ref notifications, value);
        }

       


        const string greetingMessageLastShownFileName = "GreetingMessageLastShownDate.txt";        
        const string greetingMessageEmbeddedResourceName = "SmartHotel.Clients.Core.Resources.GreetingMessage.txt";

        public ICommand NotificationsCommand => new AsyncCommand(OnNotificationsAsync);

        

        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("homeview async");
            try
            {
                IsBusy = true;
                HasBooking = AppSettings.HasBooking;

                GreenChart = await chartService.GetGreenChartAsync();

                //var authenticatedUser = authenticationService.AuthenticatedUser;
                //var notifications = await notificationService.GetNotificationsAsync(3, authenticatedUser.token);
                //Notifications = new ObservableRangeCollection<Notification>(notifications);

                ShowGreetingMessage();
            }
            catch (ConnectivityException cex)
            {
                Debug.WriteLine($"[Home] Connectivity Error: {cex}");
                await DialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Home] Error: {ex}");
                await DialogService.ShowAlertAsync(Resources.ExceptionMessage, Resources.ExceptionTitle, Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }
        }


        void ShowGreetingMessage()
        {
            // Check the last time the greeting message was showed. That date is saved in a File.
            // If the file does not exists, or the date in the file is in the past, don't show the greeting-
            if (fileService.ExistsInLocalAppDataFolder(greetingMessageLastShownFileName))
            {
                var textFromFile = fileService.ReadStringFromLocalAppDataFolder(greetingMessageLastShownFileName);
                long.TryParse(textFromFile, out var lastShownTicks);

                if (lastShownTicks < DateTime.Now.Ticks)
                {
                    return;
                }
            }

            // Show greeting message
            var greetingMessage = fileService.ReadStringFromAssemblyEmbeddedResource(greetingMessageEmbeddedResourceName);
            DialogService.ShowToast(greetingMessage);

            // Save last shown date
            var stringTicks = DateTime.Now.Ticks.ToString();
            fileService.WriteStringToLocalAppDataFolder(greetingMessageLastShownFileName, stringTicks);
        }

        public Task OnViewAppearingAsync(VisualElement view)
        {
            Console.WriteLine("homeView onviewappearingasync");
            //MessagingCenter.Subscribe<Booking>(this, MessengerKeys.BookingRequested, OnBookingRequested);
            //MessagingCenter.Subscribe<CheckoutViewModel>(this, MessengerKeys.CheckoutRequested, OnCheckoutRequested);
            return Task.FromResult(true);
        }

	    public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }


        // DOSAOLIVAR COMMANDS
        public ICommand MisParcelasCommand => new AsyncCommand(MisParcelasAsync);
        public ICommand MisEquiposCommand => new AsyncCommand(MisEquiposAsync);
        public ICommand NuevoTratamientoCommand => new AsyncCommand(NuevoTratamientoAsync);
        public ICommand AjustesCommand => new AsyncCommand(AjustesAsync);
        public ICommand TratamientosCommand => new AsyncCommand(TratamientosAsync);
        public ICommand MisTratamientosCommand => new AsyncCommand(MisTratamientosAsync);
        public ICommand CalendarioCommand => new AsyncCommand(CalendarioAsync);
        public ICommand GenerarInformeCommand => new AsyncCommand(GenerarInformeAsync);

        Task MisParcelasAsync() => NavigationService.NavigateToAsync<MisParcelasViewModel>();
        //Task NuevaParcelaAsync() => NavigationService.NavigateToAsync<NuevaParcelaViewModel>();
        Task MisEquiposAsync() => NavigationService.NavigateToAsync<ListadoEquiposViewModel>();


        async Task CalendarioAsync()
        {
            AppSettings.TratamientosStatic = await parcelasService.GetAllTratamientosAsync();
          /*  foreach (var o in AppSettings.TratamientosStatic)
            {
                Console.WriteLine("---");
                Console.WriteLine(o.nombre);

            }*/
            await NavigationService.NavigateToAsync<CalendarioTratamientosViewModel>();
        }
      


        //Task NuevoTratamientoAsync() => NavigationService.NavigateToAsync<NuevoTratamiento1ViewModel>();
        Task NuevoTratamientoAsync() => NavigationService.NavigateToAsync<NuevoTratamiento1ViewModel>();
        Task MisTratamientosAsync() => NavigationService.NavigateToAsync<ListaTratamientosViewModel>();

        async Task AjustesAsync() => await Application.Current.MainPage.DisplayAlert("Atencion", "Esta función estará disponible en futuras versiones (" + AppSettings.VersionAPP + ")", "OK");
        async Task TratamientosAsync() => await Application.Current.MainPage.DisplayAlert("Atencion", "A la espera de comprobar que los calculos son correctos", "OK");


        Task OnNotificationsAsync() => NavigationService.NavigateToAsync(typeof(NotificationsViewModel), Notifications);

       

        async Task GenerarInformeAsync()
        {
            Console.WriteLine("generar informe");
            var answerInsertar = await Application.Current.MainPage.DisplayAlert("Informe", "¿Desea que enviemos un informe a su correo electronico?", "Si", "No");
            if (answerInsertar)
            {
                var answer = await LoginRequest.GetReportAsync("https://app.dosaolivar.es/setup/exportPDF/" + AppSettings.sessionUsername);
                if (answer.success)
                {
                    await Application.Current.MainPage.DisplayAlert("Informe", "El informe se generó correctamente", "Ok");
                }
            }
        }

            void OnCheckoutRequested(object args) => HasBooking = false;
    }
}