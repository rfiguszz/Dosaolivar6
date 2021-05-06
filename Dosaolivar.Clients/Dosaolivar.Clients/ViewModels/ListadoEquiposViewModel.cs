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
    public class ListadoEquiposViewModel : ViewModelBase
    {
        // Estos habra que quitarlos
        readonly IAnalyticService analyticService;

        // DOSAOLIVAR
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        EquipoModel equipo;
        public EquipoModel Equipo
        {
            get => equipo;
            set => SetProperty(ref equipo, value);
        }


        string search;
        IEnumerable<EquipoModel> equipos;
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

      
        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }

        public ListadoEquiposViewModel()
        {

        }

        public ListadoEquiposViewModel(
        IParcelasService parcelasService,
        IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;
            this.analyticService = analyticService;
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            Console.WriteLine("Hemos llamado a ListadoEquiposMarcaViewModel");


            equipos = new List<EquipoModel>();
            suggestions = new List<string>();

            equipo = new EquipoModel();
            equipo.username = AppSettings.sessionUsername;
        }


        public override async Task InitializeAsync(object navigationData)
        {

            try
            {
                IsBusy = true;

                equipos = await parcelasService.GetAllEquiposAsync();

                Suggestions = new List<string>(equipos.Select(c => c.ToString()));
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
                    equipos.Select(c => c.getNombreCompleto())
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

        // Nuevo Equipo
      
        public ICommand NuevoEquipoCommand => new AsyncCommand(NuevoEquipoAsync);
        Task NuevoEquipoAsync() => NavigationService.NavigateToAsync<NuevoEquipoViewModel>(Equipo);

        public ICommand NextCommand => new AsyncCommand(NextAsync);

        Task NextAsync()
        {
            // FALTA
            var equiposel = equipos.FirstOrDefault(c => c.ToString().Equals(Suggestion));

            if (equiposel != null)
            {
                Console.WriteLine("equipo seleccionada: " + equiposel.id);
                /*¿ foto del equipo a enviar??? 
                 * boquilla.urlfoto = "https://app.dosaolivar.es/boquillas/" + boquilla.codfoto + ".jpg";*/
                return NavigationService.NavigateToAsync<DetalleEquipoViewModel>(equiposel);
            }
            // just return Task, but have to provide an argument because there is no overload
            return Task.FromResult(true);
        }
    }



}