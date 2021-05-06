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

namespace SmartHotel.Clients.Core.ViewModels
{

    public class DetalleTratamientoViewModel : ViewModelBase
    {
        double StepValue = 0.5;
        double Qb = 0.0;
        double QbExcel;
        double K = 0.0;
        double presion = 0.0;
        // -------  DOSAOLIVAR --------
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        public ObservableCollection<SistemaCultivoModel> ListaSistemaCultivo { get; set; }


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
                    //double Qt = StaticValues.getDoubleCalculoCaudalAplicacion(vca, parcela.marco_cultivo_id,, parcela.s_metros + "." + parcela.s_decimales, VelocidadObjetivo);
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






        public DetalleTratamientoViewModel()
        {
            //this.IsBusy = false;
            Console.WriteLine("NuevoTratamientoViewModel");
        }

        public DetalleTratamientoViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            yaentramos = false;
            TratamientoDate = DateTime.Today;
            TratamientoTime = DateTime.Now.TimeOfDay;
            this.parcelasService = parcelasService;
           // Tratamientos = new TratamientosModel();
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
            CultureInfo englishGBCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishGBCulture;
            CultureInfo.DefaultThreadCurrentUICulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = englishGBCulture;




        }


        public override async Task InitializeAsync(object navigationData)
        {
            this.IsBusy = true;
            Console.WriteLine("DetalleTratamientoViewModel ASYN1");
            parcelas = await parcelasService.GetParcelasAsync();
            equipos = await parcelasService.GetAllEquiposAsync();
            ListaParcelas = parcelas;
            ListaEquipos = equipos;
            Console.WriteLine("DetalleTratamientoViewModel ASYNC2");
            Console.WriteLine("DetalleTratamientoViewModel " + navigationData);
            if (navigationData != null)
            {
                Tratamientos = navigationData as TratamientosModel;

                // Convertir valores null a ""
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(Tratamientos))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(Tratamientos);
                    if (value == null)
                    {
                        Console.WriteLine("Encontramos null {0}={1}", name, value);
                        descriptor.SetValue(Tratamientos, "");
                        
                    }
                    else if (value.ToString() == "null")
                    {
                        Console.WriteLine("Encontramos string null {0}={1}", name, value);
                        descriptor.SetValue(Tratamientos, "");
                    }
                    GetPresion = Tratamientos.presion;
                    Tratamientos.presion = Tratamientos.presion.Length > 3 ? Tratamientos.presion.Substring(0, 3) : Tratamientos.presion;
                    OnPropertyChanged("Tratamientos");
                    IsBusy = false;
                }


                OnPropertyChanged("Tratamientos");
                //                OnPropertyChanged("SelectedFuncion1");
            }
            IsNextEnabled = true;





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
                    Console.WriteLine("selectedEquipo" + SelectedEquipo.boquilla1id);
                    return NavigationService.NavigateToAsync<DetalleEquipoViewModel>(selectedEquipo);
                }
            }
            return Task.FromResult(true);
        }



        public ICommand NuevoTratamientoCommand => new AsyncCommand(NuevoTratamientoAsync);
        public ICommand DeleteTratamientoCommand => new AsyncCommand(DeleteTratamientoAsync);
        public ICommand UpdateTratamientoCommand => new AsyncCommand(UpdateTratamientoAsync);

        async Task DeleteTratamientoAsync()
        {
            //MessagingCenter.Send(this, MessengerKeys.ActualizacionListadoParcelasSolicitado);
            var answerDelete = await Application.Current.MainPage.DisplayAlert("Borrar Tratamiento", "¿Está seguro que desea eliminar el tratamiento ?", "Si", "No");
            if (answerDelete)
            {
                Console.WriteLine("vamos a borrar tratamiento con id " + Tratamientos.id);
                var answer = await CustomRequest.DeleteTratamientoAPIModel(Tratamientos.id);
                if (answer.success)
                {
                    //await NavigationService.NavigateBackAsync();
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Ocurrió un error al borrar el tratamiento, inténtelo mas tarde", "Ok");
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
            }
        }


        async Task UpdateTratamientoAsync()
        {

            await Application.Current.MainPage.DisplayAlert("Atención", "Función disponible en proximas versiones", "Ok");
        }

        async Task NuevoTratamientoAsync()
        {
            bool validation = true;
            Console.WriteLine("para insertar tratamiento");
            if (SelectedEquipo == null || SelectedParcela == null || Tratamientos.nombre == "")
            {
                validation = false;

                await Application.Current.MainPage.DisplayAlert("Atención", "Debe de seleccionar una parcela, un equipo y definir un nombre para el tratamiento", "OK");
            }
            Console.WriteLine("eeeingg" + Tratamientos.nombre);
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

    }

}

/*
DateTime date;
try
{
    date = DateTime.Parse(Tratamientos.fecha);
    Console.WriteLine("fecha recogida" + date.ToString("MM-dd-yyyy"));
}
catch (Exception ex)
{
    Console.WriteLine($"[ERROR CONTANDO BOQUILLAS] Error: {ex}");

}*/


