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
using System.ComponentModel;
using System.Globalization;
using System.Collections.ObjectModel;

namespace SmartHotel.Clients.Core.ViewModels
{

    public class DetalleParcelaViewModel : ViewModelBase
    {
        //readonly IAnalyticService analyticService;
        CultureInfo culture;

        // -------  DOSAOLIVAR --------
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        public ObservableCollection<SistemaCultivoModel> ListaSistemaCultivo { get; set; }
        public ObservableCollection<MarcoPlantacionModel> ListaMarcoPlantacion { get; set; }
        public ObservableCollection<DensidadHojasModel> ListaDensidadHojas { get; set; }

        // FUNCIONES CALCULO GENERALES
        DOSAOLIVARStaticValues DOSAFuctions;

        // SISTEMA CULTIVO
        SistemaCultivoModel selectedSistemaCultivo;
        public SistemaCultivoModel SelectedSistemaCultivo
        {
            get => selectedSistemaCultivo;
            set => SetProperty(ref selectedSistemaCultivo, value);

        }
        // Densidad Hojas
        DensidadHojasModel selectedDensidadHojas;
        public DensidadHojasModel SelectedDensidadHojas
        {
            get => selectedDensidadHojas;
            set => SetProperty(ref selectedDensidadHojas, value);
        }

        LocalidadModel localidad;
        public LocalidadModel Localidad
        {
            get => localidad;
            set => SetProperty(ref localidad, value);
        }

        ParcelasModel parcela;
        public ParcelasModel Parcela
        {
            get => parcela;
            set => SetProperty(ref parcela, value);
        }

        public string tipomarco = "";
        public string TipoMarco
        {
            get => tipomarco;
            set => SetProperty(ref tipomarco, value);
        }


        public string tipoolivar = "";
        public string TipoOlivar
        {
            get => tipoolivar;
            set => SetProperty(ref tipoolivar, value);
        }

        public string tipodensidad = "";
        public string TipoDensidad
        {
            get => tipodensidad;
            set => SetProperty(ref tipodensidad, value);
        }


        public string densidadplantacionnha = "";
        public string DensidadPlantacionNha
        {
            get => densidadplantacionnha;
            set => SetProperty(ref densidadplantacionnha, value);
        }
      


        public string volumencopahectarea = "";
        public string VolumenCopaHectarea
        {
            get => volumencopahectarea;
            set => SetProperty(ref volumencopahectarea, value);
        }

        public string separacionarboles = "";
        public string getSeparacionArboles
        {
            get => separacionarboles + " m";
            set => SetProperty(ref separacionarboles, value);
        }

        public string anchuracalle = "";
        public string getAnchuraCalle
        {
            get => anchuracalle + " m";
            set => SetProperty(ref anchuracalle, value);
        }

        public string area = "";
        public string getArea
        {
            get => area + " ha";
            set => SetProperty(ref area, value);
        }

        public string calculovolumencopa = "";
        public string getCalculoVolumenCopa
        {
            get => calculovolumencopa;
             set => SetProperty(ref calculovolumencopa, value);
        }

        public string vca = "";
        public string getVCA
        {
            get => vca + " m³/árbol";
            set => SetProperty(ref vca, value);
        }


        public string vcopahectarea = "";
        public string getVCopaHectarea
        {
            //get => vcopahectarea + " m\u00B3/ha";
            get => vcopahectarea + " m³/ha";
            set => SetProperty(ref vcopahectarea, value);
        }

        public double  getnha = 0.0;
        public double getNha
        { 

            get =>  getnha;
            set => SetProperty(ref getnha, value);
        }

        // Para pasar a siguiente pantalla
        bool isNextEnabled;
        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }


        public DetalleParcelaViewModel()
        {
            //this.IsBusy = false;
            Console.WriteLine("DetalleParcelaViewModel");
        }

        public DetalleParcelaViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
           
            Console.WriteLine("DetalleParcelaViewModel GENERAL");
            DOSAFuctions = new DOSAOLIVARStaticValues();

            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            CultureInfo englishGBCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishGBCulture;
            CultureInfo.DefaultThreadCurrentUICulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = englishGBCulture;

            ListaSistemaCultivo = new ObservableCollection<SistemaCultivoModel>(DOSAFuctions.getSistemasCultivos());
            ListaMarcoPlantacion = new ObservableCollection<MarcoPlantacionModel>(DOSAFuctions.getMarcoPlantacion());
            ListaDensidadHojas = new ObservableCollection<DensidadHojasModel>(DOSAFuctions.getDensidadHojas());
        }




        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("DetalleParcelaViewModel ASYNC");
            if (navigationData != null)
            {
                Parcela = navigationData as ParcelasModel;

                //urlImagen = "https://app.dosaolivar.es/boquillas/" + boquilla.codfoto + ".jpg";
                Console.WriteLine("ya hemos recibido la info de parcela " + Parcela.nombre);

                TipoMarco = ListaMarcoPlantacion.FirstOrDefault(c => c.value.Equals(parcela.marco_cultivo_id)).des;
                TipoDensidad = ListaDensidadHojas.FirstOrDefault(c => c.value.Equals(parcela.densidad_hojas_id)).des;
                TipoOlivar = ListaSistemaCultivo.FirstOrDefault(c => c.value.Equals(parcela.sistema_cultivo_id)).des;

                SelectedSistemaCultivo = ListaSistemaCultivo.FirstOrDefault(c => c.value.Equals(parcela.sistema_cultivo_id));
                SelectedDensidadHojas = ListaDensidadHojas.FirstOrDefault(c => c.value.Equals(parcela.densidad_hojas_id));
                Console.WriteLine("seleccionado" + selectedSistemaCultivo.des);

                getAnchuraCalle = parcela.a_metros + "," + parcela.a_decimales;
                getSeparacionArboles = parcela.s_metros + "," + parcela.s_decimales;
                getVCA = parcela.copa_vca.Replace(".", ",");
               
                DensidadPlantacionNha = DOSAFuctions.getIntArbolesHectarea(Parcela.area, Parcela.a_metros + "." + Parcela.a_decimales, Parcela.s_metros + "." + Parcela.s_decimales, Parcela.marco_cultivo_id).ToString();
                
                double Nha = DOSAFuctions.getNha(Parcela.area, Parcela.a_metros + "." + Parcela.a_decimales, Parcela.s_metros + "." + Parcela.s_decimales, Parcela.marco_cultivo_id);
                
                Console.WriteLine("getVCA: " + parcela.copa_vca);
                VolumenCopaHectarea = DOSAFuctions.DoubleToEUString(DOSAFuctions.getDoubleVolumenCopaHectarea(Nha, parcela.copa_vca.Replace(",","."))); // Vca
                getVCopaHectarea = VolumenCopaHectarea;
                Console.WriteLine("getVCopaHectarea: " + getVCopaHectarea);
               
                getArea = parcela.area;
                 
                getCalculoVolumenCopa = "H=" + Parcela.copa_h + "m, D1=" + Parcela.copa_d1 + "m, D2=" + Parcela.copa_d2 + "m";
                Console.WriteLine("getArea: " + parcela.area);
                Console.WriteLine("id " + Parcela.id);
                Console.WriteLine("marcosel " + Parcela.getSeparacionArboles() + " " + Parcela.s_metros + " " + Parcela.s_decimales);
                
                Console.WriteLine("DensidadPlantacionNha: " + DensidadPlantacionNha);
                Console.WriteLine("FIN DE CALCULOS " + VolumenCopaHectarea);
                // Se puede eliminar
                foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(parcela))
                {
                    string name = descriptor.Name;
                    object value = descriptor.GetValue(parcela);

                    Console.WriteLine("{0}={1}", name, value);
                }
                Parcela.localidad = Parcela.localidad.Replace("&OACUTE;", "O");
                Parcela.localidad = Parcela.localidad.Replace("&Oacute;", "O");
                Parcela.localidad = Parcela.localidad.Replace("&Eacute;", "E");
                Parcela.localidad = Parcela.localidad.Replace("&EACUTE;", "E");
                Parcela.localidad = Parcela.localidad.Replace("&IACUTE;", "I");

                
                OnPropertyChanged("Parcela");
                OnPropertyChanged("SelectedSistemaCultivo");
                OnPropertyChanged("SelectedDensidadHojas");


            }
            IsNextEnabled = true;
        }

        // BORRAR E INSERTAR PARCELA
        public ICommand UpdateTratamientoCommand => new AsyncCommand(UpdateParcelaAsync);
        public ICommand DeleteParcelaCommand => new AsyncCommand(DeleteParcelaAsync);
        async Task DeleteParcelaAsync()
        {
            //MessagingCenter.Send(this, MessengerKeys.ActualizacionListadoParcelasSolicitado);
            var answerDelete = await Application.Current.MainPage.DisplayAlert("Borrar parcela", "¿Está seguro que desea eliminar la parcela?", "Si", "No");
            if (answerDelete)
            {
                Console.WriteLine("vamos a borrar parcela con id " + Parcela.id);
                var answer = await CustomRequest.DeleteParcelaAPIModel(Parcela.id);
                if (answer.success)
                {
                    //await NavigationService.NavigateBackAsync();
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Ocurrió un error al borrar la parcela, inténtelo mas tarde", "Ok");
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
            }
        }

        async Task UpdateParcelaAsync()
        {

            await Application.Current.MainPage.DisplayAlert("Atención", "Función disponible en proximas versiones", "Ok");
        }
    }

}

/*
 *
 *
 *
 * getNha = Nha;
                
                var nhastring = Nha.ToString();
                string[] namesArray = nhastring.Split('.');
                nhastring = namesArray[0];
                DensidadPlantacionNha2 = nhastring + " " +  CultureInfo.CurrentCulture;

    */