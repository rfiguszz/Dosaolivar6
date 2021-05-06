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
using System.ComponentModel;
using System.Net;
using System.IO;

namespace SmartHotel.Clients.Core.ViewModels
{

    public class NuevoEquipoViewModel : ViewModelBase
    {

        // -------  DOSAOLIVAR --------
        readonly IParcelasService parcelasService;
        readonly IDismissKeyboardService dismissKeyboardService;


        //-------   INTERFACES

        // Equipo a insertar
        EquipoModel equipo;
        public EquipoModel Equipo
        {
            get => equipo;
            set => SetProperty(ref equipo, value);
        }

        ObservableCollection<EquipoBoquillasDiscosModel> boquillas = new ObservableCollection<EquipoBoquillasDiscosModel>();

        public ObservableCollection<EquipoBoquillasDiscosModel> Boquillas { get { return boquillas; } }


        //-------   INIT 
        public NuevoEquipoViewModel(
            IParcelasService parcelasService)
        {
            // DOSAOLIVAR
            this.parcelasService = parcelasService;

            
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();
            Console.WriteLine("NuevoEquipoViewModel GENERAL");
        }


        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("NuevoEquipoViewModel ASYNC");
            MessagingCenter.Subscribe<EquipoModel>(this, MessengerKeys.BookingRequested, OnNuevoEquipoChangeRequested);
            // En ppio esto ya no hace falta que venga con datos
            // Cargamos los datos del equipo en blanco si venimos de la pantalla principal o con los datos actualizados despues de coger una boquilla
            if (navigationData != null)
            {
                Equipo = navigationData as EquipoModel;
                equipo = navigationData as EquipoModel;
                Console.WriteLine("Se carga EquipoViewModel con datos: " + Equipo.boquilla1id);
                // Asignamos el usuario
                Equipo.username = AppSettings.sessionUsername;
                // Cargamos la lista de boquillas por si estamos volviendo de insertar una antes de insertar el equipo en la bbdd
                getListaBoquillasDiscos();
                //Console.WriteLine("Ya hemos asignado la variable " + Equipo.username);
            }

        }

        void OnNuevoEquipoChangeRequested(EquipoModel _equipo ) 
        {
            if (_equipo == null)
            {
                Console.WriteLine(" errrrrorrr");
                return;
            }
            else
            {
                Console.WriteLine("acierto " + _equipo.nombre);
                Equipo = _equipo;
                equipo.nombre = _equipo.nombre;
            }

        }

        public NuevoEquipoViewModel()
        {
            //this.IsBusy = false;
            Console.WriteLine("NuevoEquipoViewModel");
        }

               
        public ICommand NuevaBoquillaCommand => new AsyncCommand(NuevaBoquillaAsync);
        async Task NuevaBoquillaAsync()
        {
            if (equipo.boquilla1id != null)
            {
                await Application.Current.MainPage.DisplayAlert("Atencion", "En esta version no se puede añadir mas de una boquilla.", "Ok");
            }
            else
            {
                if (equipo.boquilla4id != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Atencion", "No puede tener mas de cuatro tipos de boquillas distintas", "Ok");
                }
                else
                {
                    Console.WriteLine("Vamos a añadir una boquilla o disco" + Equipo.username);
                    await NavigationService.RemoveLastFromBackStackAsync();
                    await NavigationService.NavigateToAsync<ListadoBoquillasMarcaViewModel>(Equipo);
                }
            }           
        }

        // Seleccionamos boquilla para borrar

        public ICommand BorrarBoquillaCommand
        {
            get
            {
                return new Command((e) =>
                {
                    var item = (e as EquipoBoquillasDiscosModel);
                    /*var answer = await DisplayAlert("system Message", "Do you wan't to exit the App?", "Yes", "No");
                    if (answer)
                    {
                        boquillas.Remove(item);
                        Console.WriteLine("P:" + item.pos);
                        eliminarDeListaBoquillasDiscos(item.pos);
                    }*/
                    eliminaBoquillaTask(item);
                });
            }
        }

        private async Task eliminaBoquillaTask(EquipoBoquillasDiscosModel item)
        {
            var answer = await Application.Current.MainPage.DisplayAlert("¿Esta seguro?", "¿Desea borrar esta boquilla/disco del equipo?", "Si", "No");
            if (answer)
            {
                boquillas.Remove(item);
                Console.WriteLine("P:" + item.pos);
                eliminarDeListaBoquillasDiscos(item.pos);
            }
        }

        // Obtenemos la lista de boquillas actual
        private void getListaBoquillasDiscos()
        {

            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(equipo))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(equipo);
                
                Console.WriteLine("{0}={1}", name, value);
            }

            if (equipo.boquilla1id != null)
            {
                var boquilla = new EquipoBoquillasDiscosModel { des = equipo.boquilla1des, url = equipo.boquilla1url, cantidad = equipo.boquilla1cantidad, pos = "1" };
                var byteArray = new WebClient().DownloadData(equipo.boquilla1url);
                boquilla.urlImageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
                boquillas.Add(boquilla);
            }
            if (equipo.boquilla2id != null)
            {
                var boquilla = new EquipoBoquillasDiscosModel { des = equipo.boquilla2des, url = equipo.boquilla2url, cantidad = equipo.boquilla2cantidad, pos = "2" };
                var byteArray = new WebClient().DownloadData(equipo.boquilla2url);
                boquilla.urlImageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
                boquillas.Add(boquilla);
            }
            if (equipo.boquilla3id != null)
            {
                var boquilla = new EquipoBoquillasDiscosModel { des = equipo.boquilla3des, url = equipo.boquilla3url, cantidad = equipo.boquilla3cantidad, pos = "3" };
                var byteArray = new WebClient().DownloadData(equipo.boquilla3url);
                boquilla.urlImageSource = ImageSource.FromStream(() => new MemoryStream(byteArray));
                boquillas.Add(boquilla);
            }
        }


        private void eliminarDeListaBoquillasDiscos(string _pos)
        {

            Console.WriteLine("vamos a borrar la posicion " + _pos);
            if (_pos == "1")
            {
                if(equipo.boquilla2id == null)
                {
                    // No habia mas:
                    equipo.boquilla1id = null;
                    equipo.boquilla1des = "";
                    equipo.boquilla1url = "";
                    equipo.boquilla1cantidad = "";
                }
                else
                {
                    equipo.boquilla1id = equipo.boquilla2id;
                    equipo.boquilla1des = equipo.boquilla2des;
                    equipo.boquilla1url = equipo.boquilla2url;
                    equipo.boquilla1cantidad = equipo.boquilla2cantidad;
                    if (equipo.boquilla2id == null)
                    {
                        equipo.boquilla2id = null;
                        equipo.boquilla2des = "";
                        equipo.boquilla2url = "";
                        equipo.boquilla2cantidad = "";
                    }
                    else
                    {
                        equipo.boquilla2id = equipo.boquilla3id;
                        equipo.boquilla2des = equipo.boquilla3des;
                        equipo.boquilla2url = equipo.boquilla3url;
                        equipo.boquilla2cantidad = equipo.boquilla3cantidad;
                        equipo.boquilla3id = null;
                        equipo.boquilla3des = "";
                        equipo.boquilla3url = "";
                        equipo.boquilla3cantidad = "";
                    }
                }
            }
            if (_pos == "2")
            {
                if (equipo.boquilla3id == null)
                {
                    // No habia mas:
                    equipo.boquilla2id = null;
                    equipo.boquilla2des = "";
                    equipo.boquilla2url = "";
                    equipo.boquilla2cantidad = "";
                }
                else
                {
                    equipo.boquilla2id = equipo.boquilla3id;
                    equipo.boquilla2des = equipo.boquilla3des;
                    equipo.boquilla2url = equipo.boquilla3url;
                    equipo.boquilla2cantidad = equipo.boquilla3cantidad;

                    equipo.boquilla3id = null;
                    equipo.boquilla3des = "";
                    equipo.boquilla3url = "";
                    equipo.boquilla3cantidad = "";
                }
            }

        }

        public ICommand NuevoEquipoCommand => new AsyncCommand(NuevoEquipoAsync);

        async Task NuevoEquipoAsync()
        {
            bool validation = true;
            Console.WriteLine("para insertar equipo");
            // Solo se obliga a equipo
            if (equipo.fabricante == "" || equipo.fabricante == null)
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe de introducir el nombre del fabricante", "OK");
            }

            // A 
            if (equipo.capacidad == "" || equipo.capacidad == null)
            {
                equipo.capacidad = " ";
            }
            if (equipo.nombre == "" || equipo.nombre == null)
            {
                equipo.nombre = " ";
            } 
            
            if (equipo.nbastidor == "" || equipo.nbastidor == null)
            {
                equipo.nbastidor = " ";
            }

            if (equipo.nroma == "" || equipo.nroma == null)
            {
                equipo.nroma = " ";
            }
            if (equipo.nropo == "" || equipo.nropo == null)
            {
                equipo.nropo = " ";
            }
            if (equipo.boquilla1id == null)
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atención", "Debe de introducir al menos una boquilla o disco", "OK");
            }
            if(validation)
            {
                var nboquillas = TryToInt32(equipo.boquilla1cantidad) + TryToInt32(equipo.boquilla2cantidad) + TryToInt32(equipo.boquilla3cantidad) + TryToInt32(equipo.boquilla4cantidad);
                equipo.nboquillas = nboquillas.ToString();
                var answerInsertar = await Application.Current.MainPage.DisplayAlert("Nuevo equipo", "¿Desea insertar el equipo " + equipo.nombre + " con " + equipo.nboquillas + " boquillas/discos?" , "Si", "No");
                if (answerInsertar)
                {
                    // Todos los valores null a ""
                    foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(equipo))
                    {
                        string name = descriptor.Name;
                        object value = descriptor.GetValue(equipo);
                        if (value == null)
                        {
                            //Console.WriteLine("Enbcontramos nulo {0}={1}", name, value);
                            descriptor.SetValue(equipo, "null");
                        }
                    }



                    var answer = await parcelasService.PostNewEquipoAsync(equipo, AppSettings.EquiposEndpoint + "/create");
                    if (answer.success)
                    {
                        await Application.Current.MainPage.DisplayAlert("Atencion", "El equipo se creó correctamente", "Ok");
                        await NavigationService.RemoveLastFromBackStackAsync();
                        await NavigationService.NavigateToAsync<MainViewModel>();
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Atencion", "Ocurrio un error compruebe que todos los campos es tan debidamente completados", "Ok");
                    }
                }
            }

        }

        public static int TryToInt32(object value)
        {
            try
            {
                var result = Convert.ToInt32(value);
                return result;
            }
            catch
            {
                return  0;
            }
        }


    }
}



/**
 *
 * Para crear contenido dinamico en pruebas
 *
 *
 *
 *  ContentView displayedview;





    ContentView DisplayedView
        {
            get => displayedview;
            set => SetProperty(ref displayedview, value);
        }


     Images = getListaImagenes();
            Suggestions = getListaBoquillas();


     private List<string> getListaBoquillas()
        {
            List<string> lista = new List<string>();

            lista.Add("boquilla1");
            lista.Add("boquilla2");
            return lista;
        }

        private List<string> getListaImagenes()
        {
            List<string> lista = new List<string>();

            lista.Add("https://app.dosaolivar.es/boquillas/150.jpg");
            lista.Add("https://app.dosaolivar.es/boquillas/151.jpg");
            return lista;
        }


    
            foreach (EquipoBoquillasDiscosModel item in BoquillasDiscos)
            {
                Console.WriteLine(item.url);
            }




    */
