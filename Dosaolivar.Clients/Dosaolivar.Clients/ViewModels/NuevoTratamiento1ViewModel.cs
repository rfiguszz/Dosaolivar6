using SmartHotel.Clients.Core.Exceptions;
using SmartHotel.Clients.Core.Services.Analytic;
using SmartHotel.Clients.Core.Services.DismissKeyboard;
using SmartHotel.Clients.Core.Services.MisParcelas;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.Services.CustomRequest;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;


using Plugin.BLE.Abstractions;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.EventArgs;
using Plugin.BLE.Abstractions.Extensions;
using Plugin.BLE;
using Plugin.Permissions.Abstractions;
using Plugin.Settings.Abstractions;

namespace SmartHotel.Clients.Core.ViewModels
{

    public class NuevoTratamiento1ViewModel : ViewModelBase
    {

        IBluetoothLE ble;
        IAdapter adapter;
        ObservableCollection<IDevice> deviceList;


        double StepValue = 0.5;
        double Qb = 0.0;
        double QbExcel;
        double K = 0.0;
        double presion = 0.0;
        // -------  DOSAOLIVAR --------
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        public ObservableCollection<FuncionModel> ListaFuncion { get; set; }
        public ObservableCollection<MateriaActivaModel> ListaMateriaActiva { get; set; }
        DOSAOLIVARStaticValues DOSAFuctions;


        IEnumerable<ParcelasModel> parcelas { get; set; }
        IEnumerable<EquipoModel> equipos { get; set; }

        private DateTime tratamientoDate;
        public DateTime TratamientoDate
        {
            get => tratamientoDate;
            set => SetProperty(ref tratamientoDate, value);

        }

        private TimeSpan tratamientoTime;
        public TimeSpan TratamientoTime
        {
            get => tratamientoTime;
            set => SetProperty(ref tratamientoTime, value);

        }
        Boolean yaentramos;
        double velocidadobjetivo;
        public double VelocidadObjetivo
        {
            get => velocidadobjetivo;
            set
            {
                SetProperty(ref velocidadobjetivo, value);
                updateVelocidad();
            }
        }

        public void updateVelocidad()
        {
            if (yaentramos)
            {
                yaentramos = false;
            }
            else
            {
                Console.WriteLine("hola");
                yaentramos = true;
                var newStep = Math.Round(velocidadobjetivo / StepValue);
                //updateCalculosCaudal();
                VelocidadObjetivo = newStep * StepValue;
            }
        }

        ParcelasModel parcela;
        public ParcelasModel Parcela
        {
            get => parcela;
            set => SetProperty(ref parcela, value);
        }




        TratamientosModel tratamientos;
        public TratamientosModel Tratamientos
        {
            get => tratamientos;
            set => SetProperty(ref tratamientos, value);
        }

        string getAreaTratar;
        public string GetAreaTratar
        {
            get => getAreaTratar;
            set => SetProperty(ref getAreaTratar, value);
        }

        string getNumeroBoquillas;
        public string GetNumeroBoquillas
        {
            get => getNumeroBoquillas;
            set => SetProperty(ref getNumeroBoquillas, value);
        }

        string getk;
        public string GetK
        {
            get => getk;
            set => SetProperty(ref getk, value);
        }

        string getQb10Bares;
        public string GetQb10Bares
        {
            get => getQb10Bares;
            set => SetProperty(ref getQb10Bares, value);
        }


        string getvca;
        public string GetVca
        {
            get => getvca;
            set => SetProperty(ref getvca, value);
        }

        string getqt;
        public string GetQt
        {
            get => getqt;
            set => SetProperty(ref getqt, value);
        }

        string getqb;
        public string GetQb
        {
            get => getqb;
            set => SetProperty(ref getqb, value);
        }

        string getK1;
        public string GetK1
        {
            get => getK1;
            set => SetProperty(ref getK1, value);
        }

        string getPresion;
        public string GetPresion
        {
            get => getPresion + " bar";
            set => SetProperty(ref getPresion, value);
        }

        public string vcopahectarea = "";
        public string getVCopaHectarea
        {
            get => vcopahectarea + " m³/ha";
            set => SetProperty(ref vcopahectarea, value);
        }

        // Para pasar a siguiente pantalla
        bool isNextEnabled;
        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }



        public IEnumerable<ParcelasModel> listaparcelas;
        public IEnumerable<ParcelasModel> ListaParcelas
        {
            get => listaparcelas;
            set => SetProperty(ref listaparcelas, value);
        }

        public IEnumerable<EquipoModel> listaequipos;
        public IEnumerable<EquipoModel> ListaEquipos
        {
            get => listaequipos;
            set => SetProperty(ref listaequipos, value);
        }

        // PARCELA SELECCIONADA
        ParcelasModel selectedParcela;
        public ParcelasModel SelectedParcela
        {
            get => selectedParcela;
            set
            {
                if (selectedParcela != value)
                {
                    selectedParcela = value;
                    updatePickerParcela();
                }
            }
        }

        // MATERIA ACTIVA
        MateriaActivaModel selectedMateria1;
        public MateriaActivaModel SelectedMateria1
        {
            get => selectedMateria1;
            set => SetProperty(ref selectedMateria1, value);
        }

        MateriaActivaModel selectedMateria2;
        public MateriaActivaModel SelectedMateria2
        {
            get => selectedMateria2;
            set => SetProperty(ref selectedMateria2, value);
        }

        MateriaActivaModel selectedMateria3;
        public MateriaActivaModel SelectedMateria3
        {
            get => selectedMateria3;
            set => SetProperty(ref selectedMateria3, value);
        }


        // FUNCION
        FuncionModel selectedFuncion1;
        public FuncionModel SelectedFuncion1
        {
            get => selectedFuncion1;
            set => SetProperty(ref selectedFuncion1, value);
        }

        FuncionModel selectedFuncion2;
        public FuncionModel SelectedFuncion2
        {
            get => selectedFuncion2;
            set => SetProperty(ref selectedFuncion2, value);
        }

        FuncionModel selectedFuncion3;
        public FuncionModel SelectedFuncion3
        {
            get => selectedFuncion3;
            set => SetProperty(ref selectedFuncion3, value);
        }


        double vca = 0.0;
        async Task updatePickerParcela()
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(selectedParcela))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(selectedParcela);

                Console.WriteLine("{0}={1}", name, value);
            }
            GetAreaTratar = selectedParcela.area_metros + "," + selectedParcela.area_decimales;

            // Calculos....
            var StaticValues = new DOSAOLIVARStaticValues();
            Console.WriteLine("Vca0 ");
            double Nha = StaticValues.getNha(selectedParcela.area, selectedParcela.a_metros + "." + selectedParcela.a_decimales, selectedParcela.s_metros + "." + selectedParcela.s_decimales, selectedParcela.marco_cultivo_id);
            Console.WriteLine("Vca1 ");
            double VolumenCopaHectarea = StaticValues.getDoubleVolumenCopaHectarea(Nha, selectedParcela.copa_vca.Replace(",", ".")); // Vca
            getVCopaHectarea = StaticValues.DoubleToEUString(StaticValues.getDoubleVolumenCopaHectarea(Nha, selectedParcela.copa_vca.Replace(",", "."))); // Vca
            Console.WriteLine("Vca2 " + VolumenCopaHectarea);
            vca = StaticValues.getDoubleVolumenCaldoHectarea(VolumenCopaHectarea, selectedParcela.sistema_cultivo_id, selectedParcela.densidad_hojas_id);

            GetVca = StaticValues.DoubleToEUString(vca);
            Console.WriteLine("Vca3 " + GetVca);
            updateCalculosCaudal();
        }

        EquipoModel selectedEquipo;
        public EquipoModel SelectedEquipo
        {
            get => selectedEquipo;
            set
            {
                if (selectedEquipo != value)
                {
                    selectedEquipo = value;
                    updatePickerEquipo();
                }
            }
        }


        async Task updatePickerEquipo()
        {

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(selectedEquipo))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(selectedEquipo);

                Console.WriteLine("{0}={1}", name, value);
            }
            try
            {
                BoquillasModel boquilla = await parcelasService.GetOneBoquillaAsync(selectedEquipo.boquilla1id);
                GetQb10Bares = boquilla.caudal;


                Console.WriteLine("#updatePickerEquipo#caudal de la boquilla 1" + boquilla.caudal);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR cogiendo BOQUILLAS] Error: {ex}");

            }

            GetNumeroBoquillas = cuentaBoquillas();
            updateCalculosCaudal();


        }

        public ICommand UpdateCalculosCommand => new Command(updateCalculosCaudal);
        void updateCalculosCaudal()
        {
            var StaticValues = new DOSAOLIVARStaticValues();
            Console.WriteLine("updateCalculosCaudal");
            if (selectedParcela != null && selectedEquipo != null)
            {
                if (selectedParcela.id != null && selectedEquipo.id != null)
                {
                    Console.WriteLine("vamos a calcular QT GETVCA = " + GetVca + " dobule: " + vca);
                    Console.WriteLine("vamos a calcular QT" + vca);
                    var a = SelectedParcela.a_metros + "." + SelectedParcela.a_decimales;
                    var s = SelectedParcela.s_metros + "." + SelectedParcela.s_decimales;
                    Console.WriteLine("vamos a calcular QT" + VelocidadObjetivo);
                    double Qt = StaticValues.getDoubleCalculoCaudalAplicacion(vca, SelectedParcela.marco_cultivo_id, a, s, VelocidadObjetivo);
                    GetQt = StaticValues.DoubleToEUString(Qt);
                    Qb = Qt / Convert.ToDouble(GetNumeroBoquillas);
                    GetQb = StaticValues.DoubleToEUString(Qb);
                    Console.WriteLine("#UpdateCalculosCommand#caudal de la boquilla 1" + GetQb10Bares.Replace(",", "."));
                    QbExcel = Convert.ToDouble(GetQb10Bares.Replace(",", "."));
                    Console.WriteLine("#UpdateCalculosCommand#QbExcel = " + QbExcel);
                    K = QbExcel * (Math.Sqrt(10.0) / 10.0);
                    Console.WriteLine("#UpdateCalculosCommand#K = " + K);
                    GetK = StaticValues.DoubleToEUString(K);
                    presion = (Math.Pow(Qb, 2.0) / Math.Pow(K, 2.0));
                    Console.WriteLine("#UpdatePresionCommand#Presion = " + presion + " = " + Math.Pow(Qb / K, 2.0));
                    Console.WriteLine("#UpdatePresionCommand#Qb = " + Qb + " y k = " + K);
                    Console.WriteLine("#UpdatePresionCommand#Qb/k = " + Qb / K);
                    Console.WriteLine("#UpdatePresionCommand#Qb/k pow2 = " + Math.Pow((Qb / K), 2));
                    GetPresion = StaticValues.DoubleToEUString(presion);
                }
            }
        }






        public NuevoTratamiento1ViewModel()
        {
            //this.IsBusy = false;
            Console.WriteLine("NuevoTratamientoViewModel");

            //ble = CrossBluetoothLE.Current;
            //adapter = CrossBluetoothLE.Current.Adapter;
            //deviceList = new ObservableCollection<IDevice>();
           

        }

        public NuevoTratamiento1ViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            DOSAFuctions = new DOSAOLIVARStaticValues();
            yaentramos = false;
            TratamientoDate = DateTime.Today;
            TratamientoTime = DateTime.Now.TimeOfDay;
            this.parcelasService = parcelasService;
            Tratamientos = new TratamientosModel();


            ListaFuncion = new ObservableCollection<FuncionModel>(DOSAFuctions.getFuncion() );
            ListaMateriaActiva = new ObservableCollection<MateriaActivaModel>(DOSAFuctions.getMateriaActiva());


            Console.WriteLine("NuevoTratamientoViewModel GENERAL");
            Console.WriteLine("Qb inicio " + Qb + " QbExcel " + QbExcel);
            parcelas = new List<ParcelasModel>();
            equipos = new List<EquipoModel>();
            selectedParcela = new ParcelasModel();
            selectedEquipo = new EquipoModel();
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();
            getAreaTratar = "0,0 ha";
            getNumeroBoquillas = "0";
            VelocidadObjetivo = 6.0;
            GetPresion = "0";
            GetVca = "0.0";
            GetQb = "0.0";
            GetK = "0.0";
            GetVca = "0.0";

            //ble = CrossBluetoothLE.Current;
            //adapter = CrossBluetoothLE.Current.Adapter;
            //deviceList = new ObservableCollection<IDevice>();
            //var state = ble.State;
            //ble.Adapter.DeviceDiscovered += _bluetoothLe_DeviceDiscovred;
            //adapter.StartScanningForDevicesAsync();

            //Tratamientos.nombre = "Nº: " + deviceList.Count.ToString() + " state : " + state;
            Tratamientos.nombre = "";
            CultureInfo englishGBCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishGBCulture;
            CultureInfo.DefaultThreadCurrentUICulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = englishGBCulture;

        }

        private async void _bluetoothLe_DeviceDiscovred(object sender, DeviceEventArgs e)
        {
            var msg = string.Format(@"Device found: {0}
                 {1} - {2}", e.Device.Name, e.Device.Id, e.Device.Rssi);
                await Application.Current.MainPage.DisplayAlert("Atención", msg, "OK");
        }


        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("NuevoTratamientoViewModel ASYN1");
            parcelas = await parcelasService.GetParcelasAsync();
            equipos = await parcelasService.GetAllEquiposAsync();
            ListaParcelas = parcelas;
            ListaEquipos = equipos;
            Console.WriteLine("NuevoTratamientoViewModel ASYNC2");

            if (navigationData != null)
            {
                Tratamientos = navigationData as TratamientosModel;
                tratamientos = navigationData as TratamientosModel;
            }

            /*if (navigationData != null)
            {
                Parcela = navigationData as ParcelasModel;
            }*/
            // parcelas = await parcelasService.GetParcelasAsync();
            IsNextEnabled = true;
            //  this.IsBusy = false;
        }

        private string cuentaBoquillas()
        {
            var cantidad = 0;
            Console.WriteLine("CuentaBoquillas:" + selectedEquipo.boquilla1cantidad);
            try
            {
                if (SelectedEquipo.boquilla1id != "null")
                {
                    cantidad += Convert.ToInt32(selectedEquipo.boquilla1cantidad);
                }
                if (selectedEquipo.boquilla2id != "null")
                {
                    Console.WriteLine("No deberia entrar en cuentaboquillas");
                    cantidad += Convert.ToInt32(selectedEquipo.boquilla2cantidad);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR CONTANDO BOQUILLAS] Error: {ex}");

            }
            Console.WriteLine("cuentaBoquillas cantidad total" + cantidad.ToString());
            return cantidad.ToString();


        }


        public ICommand SelParcelaCommand => new AsyncCommand(SelParcelaAsync);
        Task SelParcelaAsync()
        {
            if (SelectedParcela != null)
            {
                if (SelectedParcela.id != null)
                {
                    Console.WriteLine("selectedParcela" + SelectedParcela.id);
                    return NavigationService.NavigateToAsync<DetalleParcelaViewModel>(selectedParcela);

                }
            }
            return Task.FromResult(true);
        }

        public ICommand SelEquipoCommand => new AsyncCommand(SelEquipoAsync);
        Task SelEquipoAsync()
        {
            if (SelectedEquipo != null)
            {
                if (SelectedEquipo.boquilla1id != "null")
                {
                    return NavigationService.NavigateToAsync<DetalleEquipoViewModel>(selectedEquipo);
                }
            }
            return Task.FromResult(true);
        }



        public ICommand NuevoTratamientoCommand => new AsyncCommand(NuevoTratamientoAsync);
        async Task NuevoTratamientoAsync()
        {
            bool validation = true;
            if (SelectedEquipo == null || SelectedParcela == null || Tratamientos.nombre == "")
            {
                validation = false;

                await Application.Current.MainPage.DisplayAlert("Atención", "Debe de seleccionar una parcela, un equipo y definir un nombre para el tratamiento", "OK");
            }
            if (validation)
            {
                //Tratamientos = new TratamientosModel();
                Tratamientos.fecha = TratamientoDate.ToString("MM-dd-yyyy");
                Tratamientos.hora = TratamientoTime.ToString();
                Tratamientos.fecha_fin = TratamientoDate.ToString("MM-dd-yyyy");
                Tratamientos.hora_fin = TratamientoTime.ToString();
                Tratamientos.username = AppSettings.sessionUsername;
                Tratamientos.velocidad = VelocidadObjetivo.ToString();
                Tratamientos.presion = presion.ToString();
                Tratamientos.parcela_id = SelectedParcela.id;
                Tratamientos.equipo_id = SelectedEquipo.id;
                Tratamientos.parcela_nombre = SelectedParcela.nombre;
                Tratamientos.equipo_nombre = SelectedEquipo.nombre;
                Tratamientos.const_k = K.ToString();
                Tratamientos.const_qb = Qb.ToString();
                Tratamientos.volumen_caldo = GetVca;

                if (SelectedFuncion1 != null)
                {
                    Console.WriteLine(SelectedFuncion1.value);
                    Tratamientos.funcion1id = SelectedFuncion1.value;
                    Tratamientos.funcion1des = SelectedFuncion1.des;
                }

              
                if (SelectedFuncion2 != null)
                {
                    Tratamientos.funcion2id = SelectedFuncion2.value;
                    Tratamientos.funcion2des = SelectedFuncion2.des;
                }

                if (SelectedFuncion3 != null)
                {
                    Tratamientos.funcion3id = SelectedFuncion3.value;
                    Tratamientos.funcion3des = SelectedFuncion3.des;
                }

                // MATERIA ACTIVA
                if (SelectedMateria1 != null)
                {
                    Console.WriteLine(SelectedMateria1.value);
                    Tratamientos.materia1id = SelectedMateria1.value;
                    Tratamientos.materia1des = SelectedMateria1.des;
                }

                if (SelectedMateria2 != null)
                {
                    Console.WriteLine(SelectedMateria2.value);
                    Tratamientos.materia2id = SelectedMateria2.value;
                    Tratamientos.materia2des = SelectedMateria2.des;
                }

                if (SelectedMateria3 != null)
                {
                    Console.WriteLine(SelectedMateria3.value);
                    Tratamientos.materia3id = SelectedMateria3.value;
                    Tratamientos.materia3des = SelectedMateria3.des;
                }



                var answerInsertar = await Application.Current.MainPage.DisplayAlert("Nuevo tratamiento", "¿Desea insertar el tratamiento " + Tratamientos.nombre + "?", "Si", "No");
                if (answerInsertar)
                {
                    // Todos los valores null a ""
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(Tratamientos))
                    {
                        string name = descriptor.Name;
                        object value = descriptor.GetValue(Tratamientos);
                        if (value == null)
                        {
                            Console.WriteLine("Enbcontramos nulo {0}={1}", name, value);
                            descriptor.SetValue(Tratamientos, "null");
                        }
                    }


                    var answer = await parcelasService.PostNewTratamientoAsync(Tratamientos, AppSettings.TratamientosEndpoint + "/create");
                    if (answer.success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atención", "El tratamiento se creó correctamente", "Ok");
                        await NavigationService.RemoveLastFromBackStackAsync();
                        await NavigationService.NavigateToAsync<MainViewModel>();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Atención", "Ocurrio un error compruebe que todos los campos es tan debidamente completados", "Ok");
                    }
                }
            }


        }

        public ICommand BuscarProducto1Command => new AsyncCommand(BuscarProducto1Async);
        Task BuscarProducto1Async()
        {
            tratamientos.producto_buscado = 1;
            return NavigationService.NavigateToAsync<ListadoProductosViewModel>(tratamientos);
        }

        public ICommand BuscarProducto2Command => new AsyncCommand(BuscarProducto2Async);
        Task BuscarProducto2Async()
        {
            tratamientos.producto_buscado = 2;
            return NavigationService.NavigateToAsync<ListadoProductosViewModel>(tratamientos);
        }

        public ICommand BuscarProducto3Command => new AsyncCommand(BuscarProducto3Async);
        Task BuscarProducto3Async()
        {
            tratamientos.producto_buscado = 3;
            return NavigationService.NavigateToAsync<ListadoProductosViewModel>(tratamientos);
        }
    }

}



