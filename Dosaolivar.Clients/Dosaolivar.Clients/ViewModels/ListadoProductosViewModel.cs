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
using Xamarin.Essentials;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class ListadoProductosViewModel : ViewModelBase
    {
        // Estos habra que quitarlos
        readonly IAnalyticService analyticService;

        // DOSAOLIVAR
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        string search;
        IEnumerable<Models.Producto> productos;
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

        public TratamientosModel tratamiento;
        // Equipo a insertar
        public TratamientosModel Tratamiento
        {
            get => tratamiento;
            set => SetProperty(ref tratamiento, value);
        }

        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }

        public ListadoProductosViewModel()
        {

        }

        public ListadoProductosViewModel(
        IParcelasService parcelasService,
        IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;
            this.analyticService = analyticService;
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            Console.WriteLine("Hemos llamado a ListadoProductosViewModel");


            productos = new List<Producto>();
            suggestions = new List<string>();
        }


        public override async Task InitializeAsync(object navigationData)
        {

            if (navigationData != null)
            {
                Tratamiento = navigationData as TratamientosModel;
                AppSettings.sessionTratamiento = Tratamiento;
            }
            try
            {
                IsBusy = true;

                productos = await parcelasService.GetAllProductosAsync();

                Suggestions = new List<string>(productos.Select(c => c.ToString()));
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
                    productos.Select(c => c.nombre_comercial)
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



        public ICommand SeleccionarProductoCommand => new AsyncCommand(SeleccionarProductoAsync);

        Task SeleccionarProductoAsync()
        {
            var producto = productos.FirstOrDefault(c => c.ToString().Equals(Suggestion));
            if (producto != null)
            {
                Console.WriteLine("boquilla seleccionada: " + producto.nombre_comercial);
                if (Tratamiento.producto_buscado == 1)
                    Tratamiento.producto1des = producto.nombre_comercial;
                else if (Tratamiento.producto_buscado == 2)
                    Tratamiento.producto2des = producto.nombre_comercial;
                else if (Tratamiento.producto_buscado == 3)
                    Tratamiento.producto3des = producto.nombre_comercial;

                return NavigationService.NavigateToAsync<NuevoTratamiento1ViewModel>(Tratamiento);
            }

            return Task.FromResult(true);
        }

        public ICommand OpenPdfCommand => new AsyncCommand(OpenPdfAsync);

        public Task OpenPdfAsync()
        {
            var producto = productos.FirstOrDefault(c => c.ToString().Equals(Suggestion));
            if (producto != null)
            {
                return Browser.OpenAsync(producto.url_pdf, BrowserLaunchMode.External);
            }

            return Task.FromResult(true);
        }
    }



}