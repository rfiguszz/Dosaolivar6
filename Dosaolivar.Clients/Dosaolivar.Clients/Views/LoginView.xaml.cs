﻿using SmartHotel.Clients.Core.Helpers;
using SmartHotel.Clients.Core.ViewModels;
using Xamarin.Forms;

namespace SmartHotel.Clients.Core.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            InitializeComponent();

            MessagingCenter.Subscribe<LoginViewModel>(this, MessengerKeys.SignInRequested, OnSignInRequested);
        }

        public void OpenToc(object sender, System.EventArgs e) =>
         Device.OpenUri(new System.Uri("https://google.com"));

        public void OpenPP(object sender, System.EventArgs e) =>
            Device.OpenUri(new System.Uri("https://twitter.com"));


        protected override void OnAppearing()
        {
            base.OnAppearing();

            StatusBarHelper.Instance.MakeTranslucentStatusBar(true);
        }

        void OnSignInRequested(LoginViewModel loginViewModel)
        {
            if(!loginViewModel.IsValid)
            {
                VisualStateManager.GoToState(UsernameEntry, "Invalid");
                VisualStateManager.GoToState(PasswordEntry, "Invalid");
            }
        }
    }
}