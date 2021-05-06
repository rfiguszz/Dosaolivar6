using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Analytic;
using SmartHotel.Clients.Core.Services.Authentication;
using SmartHotel.Clients.Core.Validations;
using SmartHotel.Clients.Core.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SmartHotel.Clients.Core.Helpers;
using System.Globalization;

namespace SmartHotel.Clients.Core.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {

        // Servicios
        readonly IAnalyticService analyticService;
        readonly IAuthenticationService authenticationService;


        // USER / PASS PARA Login
        ValidatableObject<string> userName;
        ValidatableObject<string> password;

        public bool IsValid { get; set; }

        public LoginViewModel(
            IAnalyticService analyticService,
            IAuthenticationService authenticationService)
        {
            this.analyticService = analyticService;
            this.authenticationService = authenticationService;

            userName = new ValidatableObject<string>();
            password = new ValidatableObject<string>();

            AddValidations();
        }

        public ValidatableObject<string> UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public ValidatableObject<string> Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }


        // Pantalla hacer login
        public ICommand SignInCommand => new AsyncCommand(SignInAsync);
        public ICommand SignGuestCommand => new AsyncCommand(SignGuestAsync);
        // Cuando se pulsa en logo para saltar login pero revisar ajustes
        //  public ICommand SettingsCommand => new AsyncCommand(NavigateToSettingsAsync);
        public ICommand SettingsCommand => new AsyncCommand(SignInAsyncAutomatic);
        // Llamamos a pantalla de registro
        public ICommand SignUpCommand => new AsyncCommand(RegistroUsuarioAsync);

        Task RegistroUsuarioAsync() => NavigationService.NavigateToAsync<RegistroUsuarioViewModel>();

        // INICIO PROCESO LOGIN INVITADO



        // INICIO PROCESO LOGIN
        async Task SignGuestAsync()
        {
            Console.WriteLine("Hemos entrado en invitado");
            await Application.Current.MainPage.DisplayAlert("Atencion", "Esta función estará disponible en futuras versiones (" + AppSettings.VersionAPP + ")", "OK");
            //string action = await Application.Current.MainPage.DisplayActionSheet("ActionSheet: SavePhoto?", "Cancel", "Delete", "Photo Roll", "Email");

        }


        // INICIO PROCESO LOGIN
        async Task SignInAsync()
        {
            IsBusy = true;
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Console.WriteLine(UserName.Value);
            Console.WriteLine(Password.Value);
            IsValid = Validate();

            // Esto se pone solo para saltarnos el control if (IsValid)
            if (true)
            {
                
                Console.WriteLine(cultureInfo.DisplayName + "/" + cultureInfo.NumberFormat + "/" + cultureInfo.NativeName);
                var userRequest = new User
                {
                    username = UserName.Value,
                    password = Password.Value,
                    token = "",
                    location = cultureInfo.DisplayName + "/" + cultureInfo.NumberFormat.NumberDecimalSeparator + "/" + cultureInfo.NativeName + "/" + cultureInfo.Name + "/" + cultureInfo.LCID
                };

               
                var user = await LoginRequest.LoginAPIModel(userRequest, AppSettings.AuthURL);
                Console.WriteLine(LogHelper.Dump(user));

                if (user.success)
                {
                    // CAMBIAR
                    AppSettings.sessionUsername = user.username;
                    AppSettings.sessionNick = user.first_name;
                    IsBusy = false;

                    analyticService.TrackEvent("SignIn");
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atencion", "El usuario o clave es incorrecto", "Ok");
                }
            }

            MessagingCenter.Send(this, MessengerKeys.SignInRequested);

            IsBusy = false;
        }



        // INICIO PROCESO AUTOMATIC
        async Task SignInAsyncAutomatic()
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            Console.WriteLine(cultureInfo.DisplayName + "/" + cultureInfo.NumberFormat.NumberDecimalSeparator + "/" + cultureInfo.NativeName);
            IsBusy = true;

            IsValid = Validate();

            // Esto se pone solo para saltarnos el control if (IsValid)
            if (true)
            {
                Console.WriteLine("0");
                var userRequest = new User
                {
                    username = "mburgos@macross.es",
                    password = "dosaolivar",
                    token = "",
                    location = cultureInfo.DisplayName + "/" + cultureInfo.NumberFormat.NumberDecimalSeparator + "/" + cultureInfo.NativeName + "/" + cultureInfo.Name + "/" + cultureInfo.LCID
                };
                var user = await LoginRequest.LoginAPIModel(userRequest, AppSettings.AuthURL);
                if (user.success)
                {
                    // CAMBIAR
                    AppSettings.sessionUsername = user.username;
                    AppSettings.sessionNick = user.first_name;
                    IsBusy = false;

                    analyticService.TrackEvent("SignIn");
                    await NavigationService.NavigateToAsync<MainViewModel>();
                }
            }

            MessagingCenter.Send(this, MessengerKeys.SignInRequested);

            IsBusy = false;
        }


        void AddValidations()
        {
            userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Username should not be empty" });
            userName.Validations.Add(new EmailRule());
            password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Password should not be empty" });
        }

        bool Validate()
        {
            var isValidUser = userName.Validate();
            var isValidPassword = password.Validate();
            return isValidUser && isValidPassword;
        }

        //Task NavigateToSettingsAsync(object obj) => NavigationService.NavigateToAsync(typeof(SettingsViewModel<RemoteSettings>));
    }
}




/**
 *
 *
 *
 *
 *
 * POR AHORA NO SE USA
 *
 *
 *
 *
 * 
 *



    

        // Para ver si muestra un formulario o no
        bool signUpForm;
        bool signInForm;







 *


// Para ver si se muestra el formulario o no de registro y de login

public ICommand ShowBetaSignInCommand => new Command(SetSignInForm);

public ICommand ShowBetaSignUpCommand => new Command(SetSignUpForm);




public bool SignUpForm
{
    get => signUpForm;
    set => SetProperty(ref signUpForm, value);
}

public bool SignInForm
{
    get => signInForm;
    set => SetProperty(ref signInForm, value);
}



void SetSignInForm()
{
    SignUpForm = false;
    SignInForm = true;
}

void SetSignUpForm()
{
    Console.WriteLine("Pulsamos para registrarnos");
    SignUpForm = true;
    SignInForm = false;
}
       */


/* Otras opciones:
* 
var isAuth = false;
var isAuth = await authenticationService.LoginAsync(UserName.Value, Password.Value);
var user = await LoginRequest.LoginAPI(UserName.Value, Password.Value, AppSettings.AuthURL); */
