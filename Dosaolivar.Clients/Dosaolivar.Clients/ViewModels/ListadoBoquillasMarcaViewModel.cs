using SmartHotel.Clients.Core.Exceptions;
using SmartHotel.Clients.Core.Services.Analytic;
using SmartHotel.Clients.Core.Services.DismissKeyboard;
using SmartHotel.Clients.Core.Services.MisParcelas;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Clients.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class ListadoBoquillasMarcaViewModel : ViewModelBase
    {
        // Estos habra que quitarlos
        readonly IAnalyticService analyticService;

        // DOSAOLIVAR
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        string search;
        IEnumerable<Models.BoquillasModel> boquillas;
        IEnumerable<string> suggestions;
        string suggestion;
        bool isNextEnabled;

        public string Search
        {
            get => search;
            set
            {
                search = value;
                FilterAsync(search);
                OnPropertyChanged();
            }
        }

        public IEnumerable<string> Suggestions
        {
            get => suggestions;
            set => SetProperty(ref suggestions, value);
        }

        public string Suggestion
        {
            get => suggestion;
            set
            {
                suggestion = value;

                IsNextEnabled = string.IsNullOrEmpty(suggestion) ? false : true;

                dismissKeyboardService.DismissKeyboard();

                OnPropertyChanged();
            }
        }

        public EquipoModel equipo;
        // Equipo a insertar
        public EquipoModel Equipo
        {
            get => equipo;
            set => SetProperty(ref equipo, value);
        }

        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }

        public ListadoBoquillasMarcaViewModel()
        {

        }

        public ListadoBoquillasMarcaViewModel(
        IParcelasService parcelasService,
        IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;
            this.analyticService = analyticService;
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            Console.WriteLine("Hemos llamado a ListadoBoquillasMarcaViewModel");


            boquillas = new List<BoquillasModel>();
            suggestions = new List<string>();
        }


        public override async Task InitializeAsync(object navigationData)
        {

            if (navigationData != null)
            {
                Equipo = navigationData as EquipoModel;
                AppSettings.sessionEquipo = Equipo;
            }
            try
            {
                IsBusy = true;

                boquillas = await parcelasService.GetAllBoquillasAsync();

                Suggestions = new List<string>(boquillas.Select(c => c.ToString()));
            }
            catch (HttpRequestException httpEx)
            {
                Debug.WriteLine($"[Booking Where Step] Error retrieving data: {httpEx}");

                if (!string.IsNullOrEmpty(httpEx.Message))
                {
                    await DialogService.ShowAlertAsync(
                        string.Format(Resources.HttpRequestExceptionMessage, httpEx.Message),
                        Resources.HttpRequestExceptionTitle,
                        Resources.DialogOk);
                }
            }
            catch (ConnectivityException cex)
            {
                Debug.WriteLine($"[Booking Where Step] Connectivity Error: {cex}");
                await DialogService.ShowAlertAsync("There is no Internet conection, try again later.", "Error", "Ok");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Booking Where Step] Error: {ex}");

                await DialogService.ShowAlertAsync(
                    Resources.ExceptionMessage,
                    Resources.ExceptionTitle,
                    Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void FilterAsync(string search)
        {
            try
            {
                IsBusy = true;

                Suggestions = new List<string>(
                    boquillas.Select(c => c.getNombreCompleto())
                           .Where(c => c.ToLowerInvariant().Contains(search.ToLowerInvariant())));

                analyticService.TrackEvent("Filter", new Dictionary<string, string>
                {
                    { "Search", search }
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Booking] Error: {ex}");
                await DialogService.ShowAlertAsync(Resources.ExceptionMessage, Resources.ExceptionTitle, Resources.DialogOk);
            }
            finally
            {
                IsBusy = false;
            }
        }



        public ICommand NextCommand => new AsyncCommand(NextAsync);

        Task NextAsync()
        {
            // FALTA
            var boquilla = boquillas.FirstOrDefault(c => c.ToString().Equals(Suggestion));

            if (boquilla != null)
            {
                Console.WriteLine("boquilla seleccionada: " + boquilla.id);
                boquilla.urlfoto = "https://dosaolivar.macross.is/panel/assets/images/dosaolivar/boquillas/" + boquilla.codfoto.TrimEnd(new char[] { '\r', '\n', ' ' }) + ".jpg";
                return NavigationService.NavigateToAsync<DetalleBoquillaViewModel>(boquilla);
            }
            // just return Task, but have to provide an argument because there is no overload
            return Task.FromResult(true);
        }
    }



}