﻿using System;
using TestDrive.Models;
using TestDrive.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestDrive
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            MessagingCenter.Subscribe<Usuario>(this, "SucessoLogin", (usuario) =>
            {
                //this.MainPage = new NavigationPage(new ListagemView());
                this.MainPage = new MasterDetailView(usuario);
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }        
       
    }
}
