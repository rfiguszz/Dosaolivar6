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
using System.Globalization;
using SmartHotel.Clients.Core.Services.AR;

namespace SmartHotel.Clients.Core.ViewModels
{

    public class NuevaParcelaPaso3ViewModel : ViewModelBase
    {
        // -------  DOSAOLIVAR --------
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

        ParcelasModel parcela;

        public ParcelasModel Parcela
        {
            get => parcela;
            set => SetProperty(ref parcela, value);
        }

        // NumMediciones
        int num_mediciones;
        public int NumMediciones
        {
            get => num_mediciones;
            set
            {
                if (num_mediciones != value)
                {
                    SetProperty(ref num_mediciones, value);
                    num_mediciones = value;
                }
            }
        }

        // CopaH
        string copa_h;
        public string CopaH
        {
            get => copa_h;
            set
            {
                if (copa_h != value)
                {
                    SetProperty(ref copa_h, value);
                    copa_h = value;
                    recalcula();
                }
            }
        }


        // CopaD1
        string copa_d1;
        public string CopaD1
        {
            get => copa_d1;
            set
            {
                if (copa_d1 != value)
                {
                    SetProperty(ref copa_d1, value);
                    copa_d1 = value;
                    recalcula();
                }
            }
        }

        // CopaD2
        string copa_d2;
        public string CopaD2
        {
            get => copa_d2;
            set
            {
                if (copa_d2 != value)
                {
                    SetProperty(ref copa_d2, value);
                    copa_d2 = value;
                    recalcula();
                }
            }
        }

        // CopaVCA
        string copa_vca;
        public string CopaVCA
        {
            get => copa_vca;
            set => SetProperty(ref copa_vca, value);
        }


        // Para pasar a siguiente pantalla
        string urlPhoto;
        public string URLPhoto
        {
            get => urlPhoto;
            set => SetProperty(ref urlPhoto, value);
        }

        public NuevaParcelaPaso3ViewModel()
        {
            //this.IsBusy = false;
            Console.WriteLine("NuevaParcelaPaso3ViewModell");
        }

        public NuevaParcelaPaso3ViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR

            this.parcelasService = parcelasService;
           
            //var StaticValues = new DOSAOLIVARStaticValues();

            // IMAGEN COPA
            URLPhoto = "https://app.dosaolivar.es/common/default.png";

            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            CultureInfo englishGBCulture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = englishGBCulture;
            CultureInfo.DefaultThreadCurrentUICulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = englishGBCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = englishGBCulture;

            Console.WriteLine("NuevaParcelaViewModel GENERAL");
        }


        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("NuevaParcelaViewModel3 ASYNC");
           
            if (navigationData != null)
            {
                Parcela = navigationData as ParcelasModel;
                NumMediciones = 0;
                CopaD1 = "0,0";
                CopaD2 = "0,0";
                CopaH = "0,0";
                CopaVCA = "0,0";
                Parcela.copa_d2 = "0,0";
                Parcela.copa_d1 = "0,0";
                Parcela.copa_h = "0,0";
                Parcela.copa_vca = "0,0";
                Console.WriteLine("ya hemos pasado a la tercera de parcela " + Parcela.nombre);
            }
            //  this.IsBusy = false;
        }

        public ICommand NuevaParcelaCommand => new AsyncCommand(NuevaParcelaAsync);


        async Task NuevaParcelaAsync()
        {
            bool validation = true;
            Console.WriteLine("Vamos a insertar con " + parcela.copa_vca);
            parcela.parcela = "";
            parcela.zona = "";
            parcela.poligono = "";
            parcela.recinto = "";
            if (parcela.id == null)
            {
                parcela.id = "null";
            }
            if (parcela.parcela == "")
            {
                parcela.parcela = " ";
            }
            if (parcela.zona == "")
            {
                parcela.zona = " ";
            }
            if (parcela.poligono == "")
            {
                parcela.poligono = " ";
            }
            if (parcela.recinto == "")
            {
                parcela.recinto = " ";
            }
            if (parcela.agregado == "")
            {
                parcela.agregado = " ";
            }
            if (validation)
            {
                var answer = await parcelasService.PostNewParcelaAsync(parcela, AppSettings.ParcelasEndpoint + "/create");
                if(answer.success)
                {
                    await Application.Current.MainPage.DisplayAlert("Atencion", "La parcela se creó correctamente", "Ok");

                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atencion", "Ocurrio un error compruebe que todos los campos es tan debidamente completados", "Ok");
                }
                Console.WriteLine(LogHelper.Dump(answer));
            }
        }


        // Actualizamos formulas

        void recalcula()
        {
            Parcela.copa_h = CopaH;
            Parcela.copa_d1 = CopaD1;
            Parcela.copa_d2 = CopaD2;
            var DOSAFuctions = new DOSAOLIVARStaticValues();
            Console.WriteLine("calculamos copa 3con valor copa_d2 " + Parcela.copa_d2 + " " + Parcela.copa_d1 + " " + Parcela.copa_h);
            Console.WriteLine("x");
            Console.WriteLine(DOSAFuctions.DoubleToEUString(DOSAFuctions.getDoubleCopaVCA(Parcela.copa_h, Parcela.copa_d1, Parcela.copa_d2)));
            Console.WriteLine("2");
            Parcela.copa_vca = DOSAFuctions.DoubleToEUString(DOSAFuctions.getDoubleCopaVCA(Parcela.copa_h, Parcela.copa_d1, Parcela.copa_d2));
            CopaVCA = Parcela.copa_vca;
        }


        void parcelaUpdated()
        {
            Console.WriteLine("Se actualizo el modelo Parcela");
        }
    }
}
