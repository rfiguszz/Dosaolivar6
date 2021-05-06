using SmartHotel.Clients.Core.Exceptions;
using SmartHotel.Clients.Core.Services.Analytic;
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
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Helpers;
using System.Collections.ObjectModel;
using System.Net;
using System.IO;

namespace SmartHotel.Clients.Core.ViewModels
{

    public class NuevaParcelaPaso2ViewModel : ViewModelBase
    {
        // -------  DOSAOLIVAR --------
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;



        public ObservableCollection<SistemaCultivoModel> ListaSistemaCultivo { get; set; }
        public ObservableCollection<MarcoPlantacionModel> ListaMarcoPlantacion { get; set; }
        public ObservableCollection<DensidadHojasModel> ListaDensidadHojas { get; set; }

        ParcelasModel parcela;

        public ParcelasModel Parcela
        {
            get => parcela;
            set => SetProperty(ref parcela, value);
        }


        // Para recoger la URL de la foto
        string urlPhoto;
        public string URLPhoto
        {
            get => urlPhoto;
            set => SetProperty(ref urlPhoto, value);
        }

        ImageSource urlPhotoImageSource;
        public ImageSource URLPhotoImageSource
        {
            get => urlPhotoImageSource;
            set => SetProperty(ref urlPhotoImageSource, value);
        }

        // Medidas A

        List<string> aPickerNumeros;
        public List<String> APickerNumeros
        {
            get => aPickerNumeros;
            set => SetProperty(ref aPickerNumeros, value);
        }

        // Medidas S

        List<string> sPickerNumeros;
        public List<String> SPickerNumeros
        {
            get => sPickerNumeros;
            set => SetProperty(ref sPickerNumeros, value);
        }

        // Decimales

        List<string> decimales;
        public List<String> PickerDecimales
        {
            get => decimales;
            set => SetProperty(ref decimales, value);
        }

      

        // SISTEMA CULTIVO
        SistemaCultivoModel selectedSistemaCultivo;
        public SistemaCultivoModel SelectedSistemaCultivo
        {
            get => selectedSistemaCultivo;
            set
            {
                if (selectedSistemaCultivo != value)
                {
                    selectedSistemaCultivo = value;
                    updatePickerSistemaCultivo();
                }
            }
        }

        // MARCO PLANTACION
        MarcoPlantacionModel selectedMarcoPlantacion;
        public MarcoPlantacionModel SelectedMarcoPlantacion
        {
            get => selectedMarcoPlantacion;
            set
            {
                if (selectedMarcoPlantacion != value)
                {
                    selectedMarcoPlantacion = value;
                    updatePhotoMarcoPlantacion();
                }
            }
        }

        // Densidad Hojas
        DensidadHojasModel selectedDensidadHojas;
        public DensidadHojasModel SelectedDensidadHojas
        {
            get => selectedDensidadHojas;
            set => SetProperty(ref selectedDensidadHojas, value);
        }


        public NuevaParcelaPaso2ViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;
            var DOSAFuctions = new DOSAOLIVARStaticValues();

            // IMAGEN
            URLPhoto = AppSettings.HostURL + "/assets/images/dosaolivar/marco_plantacion/default.png";
            var byteArray = new WebClient().DownloadData(URLPhoto);
            URLPhotoImageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
            OnPropertyChanged("URLPhotoImageSource");

            ListaSistemaCultivo = new ObservableCollection<SistemaCultivoModel>(DOSAFuctions.getSistemasCultivos());
            ListaMarcoPlantacion = new ObservableCollection<MarcoPlantacionModel>(DOSAFuctions.getMarcoPlantacion());
            ListaDensidadHojas = new ObservableCollection<DensidadHojasModel>(DOSAFuctions.getDensidadHojas());
            APickerNumeros = DOSAFuctions.getNumerosTradicional();
            SPickerNumeros = DOSAFuctions.getNumerosTradicional();
            PickerDecimales = DOSAFuctions.getNumeros();
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();
           
            
            Console.WriteLine("NuevaParcelaViewModel GENERAL");


        }


        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("NuevaParcelaPaso2ViewModel ASYNC");
            if (navigationData != null)
            {
                Parcela = navigationData as ParcelasModel;

                OnPropertyChanged("Parcela");
            }
            //  this.IsBusy = false;
        }




        public NuevaParcelaPaso2ViewModel()
        {
            //this.IsBusy = false;
            Console.WriteLine("NuevaParcelaPaso2ViewModel");
        }

        // SIGUIENTE PANTALLA
        public ICommand NuevaParcelaCommand => new AsyncCommand(NuevaParcelaAsync);

        async Task NuevaParcelaAsync()
        {
            bool validation = true;
            Console.WriteLine("Pasamos a siguiente pantalla");
            // SistemaCultivo
            if (SelectedSistemaCultivo != null)
            {
                Console.WriteLine(SelectedSistemaCultivo.value);
                Parcela.sistema_cultivo_id = SelectedSistemaCultivo.value;
            }
            else
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "Debe de seleccionar un sistema de cultivo", "Ok");
            }

            if (SelectedMarcoPlantacion != null)
            {
                Console.WriteLine(SelectedMarcoPlantacion.value);
                Parcela.marco_cultivo_id = SelectedMarcoPlantacion.value;
            }
            else
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "Debe de seleccionar un marco de Plantacion", "Ok");
            }

            if (SelectedDensidadHojas != null)
            {
                Console.WriteLine(SelectedDensidadHojas.value);
                Parcela.densidad_hojas_id = SelectedDensidadHojas.value;
            }
            else
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "Debe de seleccionar la densidad de hojas", "Ok");
            }


            if (Parcela.a_metros == "" || Parcela.a_decimales == "" || Parcela.s_metros == "" || Parcela.s_metros == "")
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "Incluya toda la informacion respectiva a la distancia (A/S)", "Ok");
            }
            if (validation)
            {
              //  Console.WriteLine("Pasamos a siguiente pantalla 3 " + Parcela.cp + " " + Parcela.localidad + " " + Parcela.sistema_cultivo_id);
                await NavigationService.NavigateToAsync<NuevaParcelaPaso3ViewModel>(Parcela);
            }
        }

        // Actualizamos photo

        void updatePhotoMarcoPlantacion()
        {
            URLPhoto = AppSettings.HostURL + "/" + selectedMarcoPlantacion.photourl;
            var byteArray = new WebClient().DownloadData(URLPhoto);
            URLPhotoImageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
            OnPropertyChanged("URLPhotoImageSource");
            parcela.a_metros = "69";
        }


        // Actualizamos photo

        void updatePickerSistemaCultivo()
        {
            var DOSAFuctions = new DOSAOLIVARStaticValues();
            if (SelectedSistemaCultivo.value == "1" || SelectedSistemaCultivo.value == "2")
            {
                APickerNumeros = DOSAFuctions.getNumerosIntensivoA();
                SPickerNumeros = DOSAFuctions.getNumerosIntensivoS();
            }
            else if (SelectedSistemaCultivo.value == "0")
            {
                APickerNumeros = DOSAFuctions.getNumerosTradicional();
            }
        }


    }
}

