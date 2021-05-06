using SmartHotel.Clients.Core.Exceptions;
using SmartHotel.Clients.Core.Services.Analytic;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.DismissKeyboard;
using SmartHotel.Clients.Core.Services.MisParcelas;
using SmartHotel.Clients.Core.ViewModels.Base;
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
    public class MisParcelasViewModel : ViewModelBase
    {
        // Estos habra que quitarlos
        readonly IAnalyticService analyticService;

        // DOSAOLIVAR
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        string search;
        IEnumerable<ParcelasModel> parcelas;
        IEnumerable<string> suggestions;
        string suggestion;
        bool isNextEnabled;

        // Nueva parcela
        public ICommand NuevaParcelaCommand => new AsyncCommand(NuevaParcelaAsync);
        Task NuevaParcelaAsync() => NavigationService.NavigateToAsync<NuevaParcelaViewModel>();

        public MisParcelasViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;


            this.analyticService = analyticService;
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            //Console.WriteLine("Hemos llamado a Mis PArcela");

            parcelas = new List<ParcelasModel>();
            suggestions = new List<string>();
            MessagingCenter.Subscribe<ParcelasModel>(this, MessengerKeys.ActualizacionListadoParcelasSolicitado, onUpdateListaParcelas);
        }



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

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                IsBusy = true;

                parcelas = await parcelasService.GetParcelasAsync();

                Suggestions = new List<string>(parcelas.Select(c => c.ToString()));
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

        void onUpdateListaParcelas(object args)
        {
            Console.WriteLine("hemos llamado al evento ActualizacionListadoParcelasSolicitado");
        }
      /*  async void onUpdateListaParcelas(object args)
        {
            Console.WriteLine("hemos llamado al evento ActualizacionListadoParcelasSolicitado");
            try
            {
                IsBusy = true;

                parcelas = await parcelasService.GetParcelasAsync();

                Suggestions = new List<string>(parcelas.Select(c => c.ToString()));
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
        }*/


        async void FilterAsync(string search)
        {
            try
            {
                IsBusy = true;

                Suggestions = new List<string>(
                    parcelas.Select(c => c.getNombreArea())
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


        public ICommand SelParcelaCommand => new AsyncCommand(SelParcelaAsync);
        Task SelParcelaAsync()
        {
            var parcelasel = parcelas.FirstOrDefault(c => c.ToString().Equals(Suggestion));

            if (parcelasel != null)
            {
                return NavigationService.NavigateToAsync<DetalleParcelaViewModel>(parcelasel);
            }
            // just return Task, but have to provide an argument because there is no overload
            return Task.FromResult(true);
        }
    }



}