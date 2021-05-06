using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.Models;
using SmartHotel.Clients.Core.Services.AR;
using SmartHotel.Clients.Core.ViewModels;
using SmartHotel.Clients.Interfaces;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class NuevaParcelaPaso3View : ContentPage
    {
      
        public NuevaParcelaPaso3View()
        {
            if (Device.RuntimePlatform != Device.iOS)
            {
                NavigationPage.SetHasNavigationBar(this, false);
            }
            BindingContext = new NuevaParcelaPaso3ViewModel();
            InitializeComponent();
                        
            NavigationPage.SetBackButtonTitle(this, "Volver");

            MessagingCenter.Subscribe<ARService, MedidasAR>(this, MessageKeys.OnMedidas, (m, medidas) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var DOSAFuctions = new DOSAOLIVARStaticValues();
                    var vm = BindingContext as NuevaParcelaPaso3ViewModel;
                    vm.NumMediciones++;

                    double copaH = 0;
                    double copaD1 = 0;
                    double copaD2 = 0;

                    copaH = (DOSAFuctions.getDoubleValue(vm.CopaH) + medidas.CopaH) / vm.NumMediciones;
                    copaD1 = (DOSAFuctions.getDoubleValue(vm.CopaD1) + medidas.CopaD1) / vm.NumMediciones;
                    copaD2 = (DOSAFuctions.getDoubleValue(vm.CopaD2) + medidas.CopaD2) / vm.NumMediciones;

                    string sCopaH = DOSAFuctions.DoubleToEUString(copaH);
                    string sCopaD1 = DOSAFuctions.DoubleToEUString(copaD1);
                    string sCopaD2 = DOSAFuctions.DoubleToEUString(copaD2);

                    vm.CopaH = sCopaH;
                    vm.CopaD1 = sCopaD1;
                    vm.CopaD2 = sCopaD2;
                });

            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<ARService>(this, MessageKeys.OnMedidas);
        }

        private void OnARMetodo1Click(object sender, EventArgs e)
        {
            DependencyService.Get<IARApp>().LaunchARMetodo1();
        }

        private void OnARMetodo2Click(object sender, EventArgs e)
        {
            DependencyService.Get<IARApp>().LaunchARMetodo2();
        }

        private void OnResetMedicionClick(object sender, EventArgs e)
        {
            var vm = BindingContext as NuevaParcelaPaso3ViewModel;
            if (vm != null)
            {
                vm.NumMediciones = 0;
                vm.CopaH = "0,0";
                vm.CopaD1 = "0,0";
                vm.CopaD2 = "0,0";
            }
        }
    }
}