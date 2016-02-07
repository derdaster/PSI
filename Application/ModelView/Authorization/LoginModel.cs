using Model.Data;
using ModelView.Base;
using ModelView.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ModelView.Authorization
{
    public class LoginModel : BaseViewModel
    {
        public event EventHandler LoginCompleted;

        private string _login;
        private string _password;

        public ICommand LoginCmd { get; set; }

        public string Login
        {
            get { return _login; }
            set
            {
                if (value == _login) return;
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value == _password) return;
                _password = value;
                OnPropertyChanged("Password");
            }
        }

        public LoginModel()
        {
            LoginCmd = new RelayCommand(par => LoginUser());

            Login = "test";
            Password = "test";
        }

        private void LoginUser()
        {
            if (!String.IsNullOrEmpty(Login) && !String.IsNullOrEmpty(Password))
            {
                if (DbManager.LoginUser(Login, MD5Helper.CreateMD5(Password)))
                {
                    RaiseLoginCompleted();
                    return;
                }
            }

            MessageBox.Show("Niepoprawny użytkownik lub hasło!");
        }

        protected virtual void RaiseLoginCompleted()
        {
            if (LoginCompleted != null)
            {
                LoginCompleted(this, EventArgs.Empty);
            }
        }
    }
}
