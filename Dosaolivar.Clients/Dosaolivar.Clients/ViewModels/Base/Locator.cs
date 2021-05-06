﻿using System;
using Autofac;
using SmartHotel.Clients.Core.Services.Analytic;
using SmartHotel.Clients.Core.Services.Authentication;
using SmartHotel.Clients.Core.Services.Chart;
using SmartHotel.Clients.Core.Services.Dialog;



// DOSAOLIVAR FILTRAR Y BORRAR
using SmartHotel.Clients.Core.Services.MisParcelas;


using SmartHotel.Clients.Core.Services.Navigation;
using SmartHotel.Clients.Core.Services.Notification;
using SmartHotel.Clients.Core.Services.OpenUri;
using SmartHotel.Clients.Core.Services.Request;
using SmartHotel.Clients.Core.Services.Settings;
using SmartHotel.Clients.Core.Services.Suggestion;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.File;
using SmartHotel.Clients.Core.Services.Geolocator;
using SmartHotel.Clients.Core.Services.IoT;

namespace SmartHotel.Clients.Core.ViewModels.Base
{
    public class Locator
    {
        IContainer container;
        ContainerBuilder containerBuilder;

        public static Locator Instance { get; } = new Locator();

        public Locator()
        {
            containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<AnalyticService>().As<IAnalyticService>();
            containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            containerBuilder.RegisterType<FakeChartService>().As<IChartService>();
            containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            containerBuilder.RegisterType<LocationService>().As<ILocationService>();
            containerBuilder.RegisterType<OpenUriService>().As<IOpenUriService>();
            containerBuilder.RegisterType<RequestService>().As<IRequestService>();
            containerBuilder.RegisterType<DefaultBrowserCookiesService>().As<IBrowserCookiesService>();
            containerBuilder.RegisterType<RoomDevicesDataService>().As<IRoomDevicesDataService>().SingleInstance();
            containerBuilder.RegisterType<GravatarUrlProvider>().As<IAvatarUrlProvider>();
            containerBuilder.RegisterType<FileService>().As<IFileService>();
            containerBuilder.RegisterType(typeof(SettingsService)).As(typeof(ISettingsService<RemoteSettings>));


            // DOSAOLIVAR
            containerBuilder.RegisterType<ParcelasService>().As<IParcelasService>();




              // BORRAR POSIBLEMENTE            
                containerBuilder.RegisterType<NotificationService>().As<INotificationService>();
                containerBuilder.RegisterType<SuggestionService>().As<ISuggestionService>();


            // DOSAOLIVAR DEFINITIVO
            containerBuilder.RegisterType<MisParcelasViewModel>();
            containerBuilder.RegisterType<DetalleParcelaViewModel>();
            containerBuilder.RegisterType<NuevaParcelaViewModel>();
            containerBuilder.RegisterType<NuevaParcelaPaso2ViewModel>();
            containerBuilder.RegisterType<NuevaParcelaPaso3ViewModel>();

            containerBuilder.RegisterType<RegistroUsuarioViewModel>();

            containerBuilder.RegisterType<ListadoBoquillasMarcaViewModel>();
            containerBuilder.RegisterType<DetalleBoquillaViewModel>();

            containerBuilder.RegisterType<NuevoEquipoViewModel>();
            containerBuilder.RegisterType<ListadoEquiposViewModel>();
            containerBuilder.RegisterType<DetalleEquipoViewModel>();

            containerBuilder.RegisterType<NuevoTratamiento1ViewModel>();
            containerBuilder.RegisterType<ListaTratamientosViewModel>();
            containerBuilder.RegisterType<DetalleTratamientoViewModel>();
            containerBuilder.RegisterType<CalendarioTratamientosViewModel>();
            containerBuilder.RegisterType<ListadoBLEViewModel>();
            containerBuilder.RegisterType<ListadoProductosViewModel>();
            // PARA BORRAR


            containerBuilder.RegisterType<HomeViewModel>();
            containerBuilder.RegisterType<LoginViewModel>();
            containerBuilder.RegisterType<MainViewModel>();
            containerBuilder.RegisterType<MenuViewModel>();
            containerBuilder.RegisterType<NotificationsViewModel>();
            containerBuilder.RegisterType<OpenDoorViewModel>();
            containerBuilder.RegisterType<SuggestionsViewModel>();

            containerBuilder.RegisterType(typeof(SettingsViewModel<RemoteSettings>));
            containerBuilder.RegisterType<ExtendedSplashViewModel>();
        }

        public T Resolve<T>() => container.Resolve<T>();

        public object Resolve(Type type) => container.Resolve(type);

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => containerBuilder.RegisterType<T>();

        public void Build() => container = containerBuilder.Build();
    }
}