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

using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Extensions;
using Plugin.BLE;
using Plugin.Permissions.Abstractions;
using Plugin.Settings.Abstractions;
using System.Collections.ObjectModel;
using Plugin.BLE.Abstractions.Exceptions;
using System.Text;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class ListadoBLEViewModel : ViewModelBase
    {
        // Estos habra que quitarlos
        readonly IAnalyticService analyticService;

        IBluetoothLE ble;
        IAdapter adapter;
        ObservableCollection<IDevice> deviceList;
        IDevice device;


        // DOSAOLIVAR
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;
        BLEModel dispositivo;
        BLEModel midispositivo;
        public BLEModel MiDispositivo
        {
            get => midispositivo;
            set => SetProperty(ref midispositivo, value);
        }


        string search;

        private ObservableCollection<BLEModel> misdispositivos;
        public ObservableCollection<BLEModel> MisDispositivos
        {
            get
            {
                return misdispositivos;
            }
            set => SetProperty(ref misdispositivos, value);
        }


       
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
        IEnumerable<string> suggestions;
        private ConnectParameters cancellationToken;

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
                Console.WriteLine("valor seleccionado " + value);
                suggestion = value;
                Console.WriteLine("valor seleccionado " + value);
                IsNextEnabled = string.IsNullOrEmpty(suggestion) ? false : true;
                Console.WriteLine("valor seleccionado " + value);
                // dismissKeyboardService.DismissKeyboard();

                OnPropertyChanged();
                Console.WriteLine("valor seleccionado " + value);
            }
        }


        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }


        // Inicializamos bluetooth
        public ListadoBLEViewModel()
        {
            Console.WriteLine("BLE");
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();
            suggestions = new List<string>();

        }

        public ListadoBLEViewModel(
        IBluetoothLE bluetoothLe, IAdapter adapter, ISettings settings, IPermissions permissions)
        {
            // DOSAOLIVAR
            Console.WriteLine("BLE2");
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();

            this.analyticService = analyticService;
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            Console.WriteLine("Hemos llamado a ListadoBLEViewModel");

            MisDispositivos = new ObservableCollection<BLEModel>();
            suggestions = new List<string>();
            suggestion = "";
            Suggestion = "";
            dispositivo = new BLEModel();
        }


        public override async Task InitializeAsync(object navigationData)
        {
            MisDispositivos = new ObservableCollection<BLEModel>();
            ble = CrossBluetoothLE.Current;
            adapter = CrossBluetoothLE.Current.Adapter;
            deviceList = new ObservableCollection<IDevice>();
            var state = ble.State;
            ble.Adapter.DeviceDiscovered += _ble_discovered;
            await adapter.StartScanningForDevicesAsync();
            await adapter.StopScanningForDevicesAsync();
            suggestion = "";

            /* try
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
             }*/
        }

        async void FilterAsync(string search)
        {
            try
            {
                IsBusy = true;

                Suggestions = new List<string>(
                    MisDispositivos.Select(c => c.getNombre())
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

        // Dispositivo seleccionado

        public ICommand NuevoEquipoCommand => new AsyncCommand(BuscarBLE);

        async Task BuscarBLE()
        {
            MisDispositivos = new ObservableCollection<BLEModel>();
            Suggestions = new List<string>();
            ble.Adapter.DeviceDiscovered += _ble_discovered;
            await adapter.StartScanningForDevicesAsync();
            await adapter.StopScanningForDevicesAsync();
            Suggestions = new List<string>(MisDispositivos.Select(c => c.getNombre()));
        }
        //   Task NuevoEquipoAsync() => NavigationService.NavigateToAsync<NuevoEquipoViewModel>(Equipo);

        public ICommand NextCommand => new AsyncCommand(NextAsync);


        public IReadOnlyList<IService> Services { get; private set; }

        private IReadOnlyList<ICharacteristic> _characteristics;
        public IReadOnlyList<ICharacteristic> Characteristics
        {
            get => _characteristics;
            private set => SetProperty(ref _characteristics, value);
        }
        async Task NextAsync()
        {
            // FALTA
            Console.WriteLine("seleccion");
            try
            {
                Console.WriteLine(Suggestion);
                var dispositivo = MisDispositivos.FirstOrDefault(c => c.ToString().Equals(Suggestion));
                try
                {
                    await adapter.ConnectToDeviceAsync(device, cancellationToken);
                    Services = await device.GetServicesAsync();
                    foreach (var X in Services)
                    {
                        Console.WriteLine("Service1 name" + X.Name);
                        Console.WriteLine("Service1 id" + X.Id);
                      //  Characteristics = await X.GetCharacteristicsAsync();
                      //  byte[] by = Encoding.ASCII.GetBytes("486f6c610d0a");
                        byte[] by = Encoding.ASCII.GetBytes("hola/r/n");
                        // var characteristic = await X.GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
                        // Console.WriteLine("hola " + characteristic.Name);
                        // var answer = await characteristic.WriteAsync(by);

                        /*
                         var bytes = await characteristic.ReadAsync();

                         await characteristic.WriteAsync(by);*/

                        foreach (var Y in Characteristics)
                          {
                              Console.WriteLine("Characteristcs NAME" + Y.Name);
                              Console.WriteLine("Characteristcs ID " + Y.Id);

                              Console.WriteLine("Characteristcs READ" + Y.CanRead);
                              Console.WriteLine("Characteristcs WRITE" + Y.CanWrite);
                              Console.WriteLine("------------ PUEDE ESCRIBIR Y LEER ------------");
                              if (Y.CanRead && Y.CanWrite)
                              {
                         
                                Console.WriteLine("ID: " + Y.Id);
                                  Console.WriteLine("ID: " + Y.Name);
                                var answer = await Y.WriteAsync(by);
                            }
                              Console.WriteLine("---------------------------");
                              /* if (Y.CanWrite)
                               {
                                   var answer = await Y.WriteAsync(by);
                                   var ra = await Y.ReadAsync();
                                   Console.WriteLine("Respuesta recibida: " + answer);
                                   Console.WriteLine("Respuesta recibida1: " + System.Text.Encoding.Default.GetString(ra));
                                   Console.WriteLine("Respuesta recibida2: " + ra.ToHexString());
                               }*/
                          }
                    }
                    /*
                    var serviceRxTx = await device.GetServiceAsync(Guid.Parse("90bce279-97e5-d723-5258-f6760dc6be35"));
                    var characteristics = await serviceRxTx.GetCharacteristicsAsync();
                    var characterRxTx = await serviceRxTx.GetCharacteristicAsync(Guid.Parse("90bce279-97e5-d723-5258-f6760dc6be35"));

                    var cw = characteristics[0].CanWrite;
                    var cr = characteristics[0].CanRead;
                    var uu = characteristics[0].Uuid;
                    byte[] by =  Encoding.ASCII.GetBytes("486f6c610d0a");

                    var wa = await characterRxTx.WriteAsync(by);
                    var ra = await characterRxTx.ReadAsync();
                    */
                    /*var service = await device.GetServicesAsync();
                    foreach (var item in service) {
                        var x = await item.GetCharacteristicsAsync();
                        Console.WriteLine(x);
                        foreach (var item2 in x)
                        {
                            Console.WriteLine(item2.GetHashCode());
                            byte[] mensaje = Encoding.ASCII.GetBytes("486f6c610d0a");
                            Console.WriteLine(item2.WriteAsync(mensaje));
                        }
                    }*/
                    //   Console.WriteLine(service);

                }
                catch (DeviceConnectionException e)
                {
                    Console.WriteLine("error en seleccionar: " + e.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error en seleccionar: " + ex.Message);
            }

            /*
            if (equiposel != null)
            {
                
                return NavigationService.NavigateToAsync<DetalleEquipoViewModel>(equiposel);
            }
            */

            // just return Task, but have to provide an argument because there is no overload
           //    return Task.FromResult(true);
        }

        private async void _ble_discovered(object sender, DeviceEventArgs e)
        {
            var msg = string.Format(@"Device found: start@{0}@end
                 {1} <> {2}", e.Device.Name, e.Device.Id, e.Device.Rssi);
            Console.WriteLine(msg);
            if (ReferenceEquals(e, null) || ReferenceEquals(e.Device.Name, null) || ReferenceEquals(e.Device.Name.Length, null))
            {
                Console.WriteLine("Device name nulo");
            }
            else
            {
                try
                {
                    Console.WriteLine("longitud: " + e.Device.Name.Length);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error en longitud: " + ex.Message);
                }
                var ble_encontrado = new BLEModel();
                ble_encontrado.name = e.Device.Name;
                ble_encontrado.id = e.Device.Id.ToString();
                ble_encontrado.rssi = e.Device.Rssi.ToString();
                MisDispositivos.Add(ble_encontrado);
                Suggestions = new List<string>(MisDispositivos.Select(c => c.getNombre()));
                if (e.Device.Id.ToString() == "90bce279-97e5-d723-5258-f6760dc6be35")
                {
                    Console.WriteLine("encontrado");
                    device = e.Device;
                }
                //await Application.Current.MainPage.DisplayAlert("Atención", msg, "OK");
            }
        }
    }



}