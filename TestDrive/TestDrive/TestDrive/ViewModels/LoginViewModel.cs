﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using TestDrive.Models;
using Xamarin.Forms;

namespace TestDrive.ViewModels
{
    public class LoginViewModel :BaseViewModel
    {
        private string usuario;

        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;                
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string senha;

        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;                
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        public ICommand EntrarCommand { get; private set; }

        public LoginViewModel()
        {
            EntrarCommand = new Command( async () =>
            {
                LoginService loginService = new LoginService();
                await loginService.FazerLogin(new Login(usuario,senha));
            }, ()=> {
                return !string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(senha);
            });
        }

        
    }
}
