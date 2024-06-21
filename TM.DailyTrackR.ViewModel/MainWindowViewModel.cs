namespace TM.DailyTrackR.ViewModel
{
    using Prism.Commands;
    using Prism.Mvvm;
    using System.Collections.ObjectModel;
    using System.Windows;
    using TM.DailyTrackR.Common;
    using TM.DailyTrackR.Logic;

    public class MainWindowViewModel : BindableBase
    {
        private string username;

        private LogicHelper helper;
        private UserAccount userAccount;

        public DelegateCommand LoginCommand { get; }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public MainWindowViewModel()
        {
            this.userAccount = new UserAccount { Username = " ", Password = " " };
            this.username = this.userAccount.Username;
            this.password = this.userAccount.Password;
            helper = new LogicHelper();
            LoginCommand = new DelegateCommand(OnLoginExecute, OnLoginCanExecute);
        }

        private bool OnLoginCanExecute()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) ;
        }

        private void OnLoginExecute()
        {
            bool isValidUser = helper.LoginController.ValidateUser(Username, Password);
            if(this.Password == " " && this.Username == " ")
            {
                MessageBox.Show("Empty username or empty password!");
            }

            else if (isValidUser)
            {
                MessageBox.Show($"Logged in as: {Username}");
                ViewService.Instance.ShowWindow(new CalendarPageViewModel());
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }
    }
}

