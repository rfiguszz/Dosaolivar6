using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.Authentication;
using SmartHotel.Clients.Core.ViewModels;
using SmartHotel.Clients.Core.ViewModels.Base;
using SmartHotel.Clients.Core.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Services.Navigation
{
    public partial class NavigationService : INavigationService
    {
        readonly IAuthenticationService authenticationService;
        protected readonly Dictionary<Type, Type> mappings;

        protected Application CurrentApplication => Application.Current;

        public NavigationService(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
            mappings = new Dictionary<Type, Type>();

            CreatePageViewModelMappings();
        }

        public async Task InitializeAsync()
        {
            if (await authenticationService.UserIsAuthenticatedAndValidAsync())
            {
                await NavigateToAsync<MainViewModel>();
            }
            else
            {
                await NavigateToAsync<LoginViewModel>();
            }
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), null);

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase => InternalNavigateToAsync(typeof(TViewModel), parameter);

        public Task NavigateToAsync(Type viewModelType) => InternalNavigateToAsync(viewModelType, null);

        public Task NavigateToAsync(Type viewModelType, object parameter) => InternalNavigateToAsync(viewModelType, parameter);

        public async Task NavigateBackAsync()
        {
            if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;
                await mainPage.Detail.Navigation.PopAsync();
            }
            else if (CurrentApplication.MainPage != null)
            {
                await CurrentApplication.MainPage.Navigation.PopAsync();
            }
        }

        public virtual Task RemoveLastFromBackStackAsync()
        {
            if (CurrentApplication.MainPage is MainView mainPage)
            {
                mainPage.Detail.Navigation.RemovePage(
                    mainPage.Detail.Navigation.NavigationStack[mainPage.Detail.Navigation.NavigationStack.Count - 2]);
            }

            return Task.FromResult(true);
        }

        protected virtual async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            //Console.WriteLine("InternalNavigateToAsync");
            var page = CreateAndBindPage(viewModelType, parameter);

            if (page is MainView)
            {
                CurrentApplication.MainPage = page;
            }
            else if (page is LoginView)
            {
                CurrentApplication.MainPage = new CustomNavigationPage(page);
            }
            else if (CurrentApplication.MainPage is MainView)
            {
                var mainPage = CurrentApplication.MainPage as MainView;

                if (mainPage.Detail is CustomNavigationPage navigationPage)
                {
                    var currentPage = navigationPage.CurrentPage;

                    if (currentPage.GetType() != page.GetType())
                    {
                        await navigationPage.PushAsync(page);
                    }
                }
                else
                {
                    navigationPage = new CustomNavigationPage(page);
                    mainPage.Detail = navigationPage;
                }

                mainPage.IsPresented = false;
            }
            else
            {
                if (CurrentApplication.MainPage is CustomNavigationPage navigationPage)
                {
                    await navigationPage.PushAsync(page);
                }
                else
                {
                    CurrentApplication.MainPage = new CustomNavigationPage(page);
                }
            }

            await (page.BindingContext as ViewModelBase).InitializeAsync(parameter);
        }

        protected Type GetPageTypeForViewModel(Type viewModelType)
        {
            if (!mappings.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return mappings[viewModelType];
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter)
        {
            //Console.WriteLine("hola");
            var pageType = GetPageTypeForViewModel(viewModelType);

            if (pageType == null)
            {
                throw new Exception($"Mapping type for {viewModelType} is not a page");
            }

            var page = Activator.CreateInstance(pageType) as Page;
            var viewModel = Locator.Instance.Resolve(viewModelType) as ViewModelBase;
            page.BindingContext = viewModel;

            return page;
        }

        void CreatePageViewModelMappings()
        {

            // DOSAOLIVAR
            mappings.Add(typeof(MisParcelasViewModel), typeof(MisParcelasView));
            mappings.Add(typeof(DetalleParcelaViewModel), typeof(DetalleParcelaView));
            mappings.Add(typeof(NuevaParcelaViewModel), typeof(NuevaParcelaView));
            mappings.Add(typeof(NuevaParcelaPaso2ViewModel), typeof(NuevaParcelaPaso2View));
            mappings.Add(typeof(NuevaParcelaPaso3ViewModel), typeof(NuevaParcelaPaso3View));

            mappings.Add(typeof(RegistroUsuarioViewModel), typeof(RegistroUsuarioView));

            mappings.Add(typeof(ListadoBoquillasMarcaViewModel), typeof(ListadoBoquillasMarcaView));
            mappings.Add(typeof(DetalleBoquillaViewModel), typeof(DetalleBoquillaView));

            mappings.Add(typeof(NuevoEquipoViewModel), typeof(NuevoEquipoView));
            mappings.Add(typeof(ListadoEquiposViewModel), typeof(ListadoEquiposView));
            mappings.Add(typeof(DetalleEquipoViewModel), typeof(DetalleEquipoView));

            mappings.Add(typeof(NuevoTratamiento1ViewModel), typeof(NuevoTratamiento1View));
            mappings.Add(typeof(ListaTratamientosViewModel), typeof(ListaTratamientosView));
            mappings.Add(typeof(DetalleTratamientoViewModel), typeof(DetalleTratamientoView));
            mappings.Add(typeof(CalendarioTratamientosViewModel), typeof(CalendarioTratamientosView));
            mappings.Add(typeof(ListadoProductosViewModel), typeof(ListadoProductosView));

            mappings.Add(typeof(ListadoBLEViewModel), typeof(ListadoBLEView));
            // A ELIMINAR   

            mappings.Add(typeof(LoginViewModel), typeof(LoginView));
            mappings.Add(typeof(MainViewModel), typeof(MainView));
            mappings.Add(typeof(NotificationsViewModel), typeof(NotificationsView));
            mappings.Add(typeof(SettingsViewModel<RemoteSettings>), typeof(SettingsView));
            mappings.Add(typeof(ExtendedSplashViewModel), typeof(ExtendedSplashView));

            if (Device.Idiom == TargetIdiom.Desktop)
            {
                mappings.Add(typeof(HomeViewModel), typeof(UwpHomeView));
                mappings.Add(typeof(SuggestionsViewModel), typeof(UwpSuggestionsView));
            }
            else
            {
                mappings.Add(typeof(HomeViewModel), typeof(HomeView));
                mappings.Add(typeof(SuggestionsViewModel), typeof(SuggestionsView));
            }
        }
    }
}