using SmartHotel.Clients.Core.Exceptions;
using SmartHotel.Clients.Core.Services.Analytic;
using SmartHotel.Clients.Core.Services.DismissKeyboard;
using SmartHotel.Clients.Core.Services.MisParcelas;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.Net;
using System.IO;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class DetalleBoquillaViewModel : ViewModelBase
    {
        // Estos habra que quitarlos
        readonly IAnalyticService analyticService;

        // DOSAOLIVAR
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;

       BoquillasModel boquilla;
        public string urlImagen;


        public BoquillasModel Boquilla
        {
            get => boquilla;
            set => SetProperty(ref boquilla, value);
        }

        ImageSource urlPhotoImageSource;
        public ImageSource URLPhotoImageSource
        {
            get => urlPhotoImageSource;
            set => SetProperty(ref urlPhotoImageSource, value);
        }


        public string cantidadboquillas;
        public string CantidadBoquillas
        {
            get => cantidadboquillas;
            set => SetProperty(ref cantidadboquillas, value);
        }


        public DetalleBoquillaViewModel(
            IParcelasService parcelasService,
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;
            this.analyticService = analyticService;
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();
            Console.WriteLine("Hemos llamado a DetalleBoquillaViewModel");
        }

        public DetalleBoquillaViewModel()
        {

        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                Boquilla = navigationData as BoquillasModel;

                //urlImagen = "https://app.dosaolivar.es/boquillas/" + boquilla.codfoto + ".jpg";
                //TestImageUri = new Uri(urlImagen);
                Console.WriteLine("ya hemos recibido la info en otra pantalla de la boquilla bk " + Boquilla.urlfoto);

                var byteArray = new WebClient().DownloadData(Boquilla.urlfoto);
                URLPhotoImageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
                OnPropertyChanged("URLPhotoImageSource");
            }

        }

        public ICommand ConfirmarBoquillaCommand => new AsyncCommand(ConfirmarBoquillaAsync);

        async Task ConfirmarBoquillaAsync()
        {
            Console.WriteLine("nboquillas " + CantidadBoquillas);
            if (CantidadBoquillas == "" || CantidadBoquillas == "0" || CantidadBoquillas == null)
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Introduzca el numero de boquillas/discos", "Ok");

            }
            else
            {
                if (AppSettings.sessionEquipo.boquilla1id == null)
                {
                    AppSettings.sessionEquipo.boquilla1id = Boquilla.id;
                    AppSettings.sessionEquipo.boquilla1des = Boquilla.marca + " " + Boquilla.modelo;
                    AppSettings.sessionEquipo.boquilla1url = boquilla.urlfoto;
                    AppSettings.sessionEquipo.boquilla1cantidad = CantidadBoquillas;
                }
                else
                {
                    if (AppSettings.sessionEquipo.boquilla2id == null)
                    {
                        AppSettings.sessionEquipo.boquilla2id = Boquilla.id;
                        AppSettings.sessionEquipo.boquilla2des = Boquilla.marca + " " + Boquilla.modelo;
                        AppSettings.sessionEquipo.boquilla2url = boquilla.urlfoto;
                        AppSettings.sessionEquipo.boquilla2cantidad = CantidadBoquillas;
                    }
                    else
                    {
                        if (AppSettings.sessionEquipo.boquilla3id == null)
                        {
                            AppSettings.sessionEquipo.boquilla3id = Boquilla.id;
                            AppSettings.sessionEquipo.boquilla3des = Boquilla.marca + " " + Boquilla.modelo;
                            AppSettings.sessionEquipo.boquilla3url = boquilla.urlfoto;
                            AppSettings.sessionEquipo.boquilla3cantidad = CantidadBoquillas;
                        }
                    }
                }
                await NavigationService.NavigateToAsync<NuevoEquipoViewModel>(AppSettings.sessionEquipo);
            }
           
        }
    }



}