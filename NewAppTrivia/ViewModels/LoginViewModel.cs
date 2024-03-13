
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
            LoginCommand = new Command(async () => await Login(), () => !String.IsNullOrEmpty(UserName) && !String.IsNullOrEmpty(Password));


        }

        public void Cancel()
        {
            UserName=string.Empty;
            Password=string.Empty;
            Messege = string.Empty;
            
            

        }

        private void CheckCommand()
        {
            ((Command)CancelCommand).ChangeCanExecute();
            ((Command)LoginCommand).ChangeCanExecute();
        }


        public async Task Login()
        {

            bool isExist = service.Login(userName, password);
            if (isExist == true)
            {
                Messege = "התחבר בהצלחה";
                Color = Colors.Green;
                AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
                await AppShell.Current.GoToAsync("BestScoresPage");

            }

            else
            {
                Messege = "לא קיים משתמש";
                Color = Colors.Red;

            }
        }


        


    }
}
