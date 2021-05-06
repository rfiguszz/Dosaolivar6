using SmartHotel.Clients.Core.Exceptions;
using SmartHotel.Clients.Core.Services.Analytic;
using SmartHotel.Clients.Core.Services.DismissKeyboard;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Clients.Core.Services.Authentication;
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
    public class RegistroUsuarioViewModel : ViewModelBase
    {
        readonly IAnalyticService analyticService;
        private User user = new User();
        // DOSAOLIVAR
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }



        readonly IDismissKeyboardService dismissKeyboardService;

        string search;
        IEnumerable<string> suggestions;
        string suggestion;
        bool isNextEnabled;



        public RegistroUsuarioViewModel(
            IAnalyticService analyticService)
        {
            // DOSAOLIVAR
            this.analyticService = analyticService;
            dismissKeyboardService = DependencyService.Get<IDismissKeyboardService>();

            Console.WriteLine("Hemos llamado a REGISTRO USUARIO ViewModel");

        }


        public RegistroUsuarioViewModel()
        {
            Console.WriteLine("RegistroUsuario");
        }


        // NUEVA USUARIO

        public ICommand SignUpCommand => new AsyncCommand(RegistroUsuarioAsync);

        async Task RegistroUsuarioAsync()
        {
            bool validation = true;
            Console.WriteLine("hemos llamado a registrarse");
            // NOMBRE Y APELLIDOS
            if (user.first_name != null && user.last_name != null)
            {
                if (user.first_name.Length < 3 || user.last_name.Length < 3)
                {
                    Console.WriteLine("longitud: " + user.first_name.Length);
                    validation = false;
                    Console.WriteLine("2");

                    await Application.Current.MainPage.DisplayAlert("Atencion", "Su nombre y apellidos no pueden estar vacios", "Ok");
                }
            }
            else
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "Su nombre y apellidos no pueden estar vacios", "Ok");
            }



            // USUARIO

            if (user.username != null)
            {
                if (user.username.Length < 8)
                {
                    Console.WriteLine("longitud username: " + user.username);
                    validation = false;
                    Console.WriteLine("3");

                    await Application.Current.MainPage.DisplayAlert("Atencion", "Su usuario debe de ser una direccion de correo electronica", "Ok");
                }
            }
            else
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "Su usuario debe de ser una direccion de correo electronica", "Ok");
            }

            // PASSWORD
            if (user.password != null && user.confirm_password != null)
            {
                if (user.password != user.confirm_password)
                {

                    Console.WriteLine("1");
                    validation = false;
                    await Application.Current.MainPage.DisplayAlert("Atencion", "Sus contraseñas no coinciden", "Ok");
                }
            }
            else
            {
                validation = false;
                await Application.Current.MainPage.DisplayAlert("Atencion", "No ha introducido la contraseña correctamente", "Ok");
            }

            // ENVIAMOS
            if (validation)
            {
                var mirespuesta = await Application.Current.MainPage.DisplayAlert("Proteccion de Datos", "Al aceptar el registro en DOSAOlivar acepta ceder sus datos con fines meramente estadisticos a Desarrollo Tecnologico Agroindustrial EBT y la Universidad de Cordoba. Si continua acepta expresamente la cesión de datos para la generación de estadisticas.", "Si", "No");
                if (mirespuesta)
                {
                    Console.WriteLine("adelaaaante");
                    var userRequest = new Models.User
                    {
                        username = user.username,
                        password = user.password,
                        token = "",
                        first_name = user.first_name,
                        last_name = user.last_name

                    };

                    var answer = await LoginRequest.RegisterUserAPIModel(userRequest, AppSettings.SignUpURL);
                    if (answer.success)
                    {
                        user.username = "";
                        await Application.Current.MainPage.DisplayAlert("Registro realizado con exito", "Su registro se ha completado con exito, acceda ahora con sus datos a la aplicacion", "OK");

                        await NavigationService.NavigateToAsync<LoginViewModel>();

                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Atencion", answer.message, "Ok");
                    }
                }
            }
            /*var validation = true;
        await DialogService.ShowAlertAsync(
              "x",
              "peep",
               Resources.DialogOk);
        Console.WriteLine("longitud: " + user.first_name.Length);
        if (user.first_name.Length < 3 || user.last_name.Length < 3)
        {
            validation = false;
            Console.WriteLine("2");

            //await Application.Current.MainPage.DisplayAlert("Atencion", "Su nombre no puede estar vacio", "Ok");
        }
        if (user.password != user.confirm_password)
        {

            Console.WriteLine("1");
            validation = false;
            //await Application.Current.MainPage.DisplayAlert("Atencion", "Sus contraseñas no coinciden", "Ok");
        }
        if (validation)
        {
            Console.WriteLine("ultima llamada");
            var mirespuesta = await Application.Current.MainPage.DisplayAlert("Proteccion de Datos", "Al aceptar el registro en DOSAOlivar acepta ceder sus datos con fines meramente estadisticos a Desarrollo Tecnologico Agroindustrial EBT y la Universidad de Cordoba. Si continua acepta expresamente la cesión de datos para la generación de estadisticas.", "Si", "No");
            if (mirespuesta)
            {
                var userRequest = new Models.User
                {
                    username = user.username,
                    password = user.password,
                    token = "",
                    first_name = user.first_name,
                    last_name = user.last_name

                };

                var answer = await LoginRequest.RegisterUserAPIModel(userRequest, AppSettings.SignUpURL);
                if (answer.success)
                {
                    user.username = "";
                    await Application.Current.MainPage.DisplayAlert("Registro realizado con exito", "Su registro se ha completado con exito, acceda ahora con sus datos a la aplicacion", "OK");

                    await NavigationService.NavigateToAsync<LoginViewModel>();

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atencion", answer.message, "Ok");
                }

            }
        } */

        }





        public IEnumerable<string> Suggestions
        {
            get => suggestions;
            set => SetProperty(ref suggestions, value);
        }



        public bool IsNextEnabled
        {
            get => isNextEnabled;
            set => SetProperty(ref isNextEnabled, value);
        }

        public ICommand NextCommand => new AsyncCommand(NextAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            Console.WriteLine("Hemos llamado a HTTP LISTAR PARCELAS");
            try
            {
                IsBusy = true;

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


        Task NextAsync()
        {
            // FALTA
            /*var city = cities.FirstOrDefault(c => c.ToString().Equals(Suggestion));

            if (city != null)
            {
                return NavigationService.NavigateToAsync<BookingCalendarViewModel>(city);
            }*/
            // just return Task, but have to provide an argument because there is no overload
            return Task.FromResult(true);
        }
    }
}