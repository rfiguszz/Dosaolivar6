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

namespace SmartHotel.Clients.Core.ViewModels
{

    public class NuevaParcelaViewModel : ViewModelBase
    {
        //readonly IAnalyticService analyticService;

        // -------  DOSAOLIVAR --------
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;
        DOSAOLIVARStaticValues DOSAFuctions;


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

        // Para recoger y llamar al CP
        string cp;
        public string CP
        {
            get => cp;
            set
            {

                if (this.cp != value)
                {
                    this.cp = value;
                    searchLocalidad();
                }
            }
        }


        // Para pasar a siguiente pantalla
        bool isNextEnabled;
        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }

        public ICommand NextCommand => new AsyncCommand(NextAsync);

        // Cuando llega a 5 hace una busqueda
        public void searchLocalidad()
        {
            if (cp.Length == 5)
            {
                IsBusy = true;
                Console.WriteLine("Recogemos provincia");
                Task.Run(async () =>
                {
                    try
                    {
                        Localidad = await parcelasService.GetManualLocalidadByCPAsync(cp);
                        IsBusy = false;
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        IsBusy = false;
                    }
                });
            }
        }


        public NuevaParcelaViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;
            localidad = new LocalidadModel();
            parcela = new ParcelasModel();
            DOSAFuctions = new DOSAOLIVARStaticValues();

            // ResetValues
            Parcela.area_metros = "0";
            Parcela.area_decimales = "0";
            Parcela.provincia = "";
            Parcela.parcela = "";
            Parcela.zona = "";
            Parcela.poligono = "";
            Parcela.agregado = "";
            Parcela.recinto = "";
            Parcela.altura_media_copa = "0.0";
            Parcela.diametro_media_copa = "0.0";
            Parcela.a_decimales = "0";
            Parcela.s_decimales = "0";
            Parcela.a_metros = "";
            Parcela.s_metros = "";
            Parcela.densidad_hojas_id = "0";
            Parcela.sistema_cultivo_id = "0";
            Parcela.marco_cultivo_id = "0";
            OnPropertyChanged("Parcela");



            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();
            Console.WriteLine("NuevaParcelaViewModel GENERAL");
        }


        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("NuevaParcelaViewModel ASYNC");
            IsNextEnabled = true;
            //  this.IsBusy = false;
        }



        public NuevaParcelaViewModel()
        {
            //this.IsBusy = false;
            Console.WriteLine("NuevaParcelaViewModel");
        }


        public ICommand NuevaParcelaCommand => new AsyncCommand(NuevaParcelaAsync);


        async Task NuevaParcelaAsync()
        {
            bool validation = true;
            Console.WriteLine("Pasamos a siguiente pantalla" + Parcela.area_metros);
            // AREA FILTER
            if (!DOSAFuctions.IsDigitsOnly(Parcela.area_metros) || !DOSAFuctions.IsDigitsOnly(Parcela.area_decimales))
            {
                validation = false;
                Parcela.area_metros = "0";
                Parcela.area_decimales = "0";
                OnPropertyChanged("Parcela");
                await Application.Current.MainPage.DisplayAlert("Atencion", "Solo se admiten caracteres numericos en el area de la parcela", "Ok");
            }
            if ((Parcela.area_metros == "0" && Parcela.area_decimales == "0") || (Parcela.area_metros == "" && Parcela.area_decimales == ""))
            {
                validation = false;
                
                await Application.Current.MainPage.DisplayAlert("Atencion", "La parcela debe de tener algun tipo de extension.", "Ok");
            }
            if (localidad.cp != null)
            {
                if (localidad.cp.Length < 5)
                {
                    validation = false;
                    await Application.Current.MainPage.DisplayAlert("Atencion", "Debe de introducir el CP", "Ok");
                }
            }
            else
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "Debe de introducir el codigo postal", "Ok");
            }

            Console.WriteLine(validation);
            if (validation)
            {
                Parcela.localidad = Localidad.localidad;
                Parcela.cp = this.CP;
                Parcela.area = Parcela.area_metros + "." + Parcela.area_decimales;
                Parcela.provincia = Localidad.provincia;
                Parcela.username = AppSettings.sessionUsername;
                // Console.WriteLine("Pasamos a siguiente pantalla 2 " + Parcela.cp + " " + Parcela.localidad);
                await NavigationService.NavigateToAsync<NuevaParcelaPaso2ViewModel>(Parcela);
            }
        }

        private async Task<bool> check()
        {
            bool validation = true;
            await Application.Current.MainPage.DisplayAlert("Atencion", "Debe de introducir el CP", "Ok");

            return validation;
        }

        // Pasamos a siguiente pantalla
        Task NextAsync()
        {

            return Task.FromResult(true);
        }

    }
}


/*
 *
 * /*     localidad.provincia = "cordoba";
            Console.WriteLine("cp: " + cp);
            LocalidadModel l = await parcelasService.GetManualLocalidadByCPAsync("41500");
            Console.WriteLine("pepe2");
            if (cp.Length == 5)
            {
                // this.localidad = await parcelasService.GetManualLocalidadByCPAsync(cp);
                this.localidad = await parcelasService.GetLocalidadByCPAsync(cp);
                Console.WriteLine("localidad recibida" + localidad.localidad);
            } */





/*  PAra metods mirar
 *  public string Search
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
   */


/*
            Console.WriteLine("NuevaParcelaAsync");
        var newParcelaRequest = new Models.ParcelasModel
        {
            username = "admin@admin.com",
            nombre = "pepe",
            area = "gitshell",
            };

            //a esto se llamaba antes: var isAuth = await authenticationService.LoginAsync(UserName.Value, Password.Value);
            // var user = await LoginRequest.LoginAPI(UserName.Value, Password.Value, AppSettings.AuthURL);
            var answer = await parcelasService.PostNewParcelaAsync(newParcelaRequest, AppSettings.ParcelasEndpoint + "/create");
            Console.WriteLine(LogHelper.Dump(answer));

        }

        private List<Provincias> GetProvincias()
    {
        Provincias x = new Provincias();
        List<Provincias> productItems = new List<Provincias>();
        productItems.Add(new Provincias()
        {
            Name = "0",
            JobsId = "Avatar.png",
           Location = "pepe"
        });
        productItems.Add(new Provincias()
        {
            Name = "1",
            JobsId = "Avatar.png",
            Location = "pepe2"
        });
        Console.WriteLine(productItems.Count);
        return productItems;


    }






*/



//Task NuevaParcelaAsync() => NavigationService.NavigateToAsync<MisParcelasViewModel>();


/* PARA EL SELECT
 List<string> _department;
    public List<string> Department
    {
        get { return _department; }
        private set
        {
            _department = value;
            OnPropertyChanged();
        }
    }

    private string _selectedDepartment = "Department of Computer Science and Engineering";
    public string SelectedDepartment
    {
        get { return _selectedDepartment; }
        set
        {

            _selectedDepartment = value;
            OnPropertyChanged();

        }
    }


    //listaProvincias = new List<Models.Provincias>();
//listaProvincias = GetProvincias();
_department = new List<string>()
        {
            "Department of Computer Science and Engineering",
            "Department of Electronics and Communication Engineering",
            "Department of Electrical and Electronics Engineering",
            "Department of Information Technology",
            "Department of Civil Engineering",
            "Department of Mechanical Engineering",
            "Department of BioTechnology"
        };


    */
