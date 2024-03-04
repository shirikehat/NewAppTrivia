
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using NewAppTrivia.Services;

namespace NewAppTrivia.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private TriviaServices service;
        private string userName;
        public string UserName
        {
            get { return this.userName ; }
            set
            {
                this.userName=value;
                OnPropertyChanged();
                CheckCommand();
            }
        }

        private string password;
        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                OnPropertyChanged();
                CheckCommand();
            }
        }
        private string messege;
        public string Messege { get { return messege; } set { messege = value; OnPropertyChanged(); } }

        public Color Color { get { return color; } set { color = value; OnPropertyChanged(); } }
        private Color color;
      
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public LoginViewModel(TriviaServices service)
        {
            this.service = service;
            CancelCommand=new Command(Cancel,()=>!string.IsNullOrEmpty(UserName)||!string.IsNullOrEmpty(Password));
            LoginCommand = new Command(Login, () =>!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));
        }

        public void Cancel()
        {
            UserName=string.Empty;
            Password=string.Empty;

        }

        private void CheckCommand()
        {
            ((Command)CancelCommand).ChangeCanExecute();
            ((Command)LoginCommand).ChangeCanExecute();
        }
        private void Login()
        {
            TriviaServices tr= new TriviaServices();
            bool result = tr.Login(UserName, Password);
            if (result)
            {
                Messege = "Success login";
                Color = Colors.Green;
            }
            else
            {
                Messege = "failed login";
                Color = Colors.Red;
            }
        }


    }
}
