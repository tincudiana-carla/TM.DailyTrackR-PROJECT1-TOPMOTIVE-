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
        private UserAccount userAccount;
        private string username;
        private string password;
        private LogicHelper helper;

        public DelegateCommand LoginCommand { get; }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public MainWindowViewModel()
        {
            userAccount = new UserAccount { Username = "Marcel", Password = "pita" };
            Username = userAccount.Username;
            Password = userAccount.Password;
            LoginCommand = new DelegateCommand(OnLoginExecute, OnLoginCanExecute);
        }

        private bool OnLoginCanExecute()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private void OnLoginExecute()
        {
            MessageBox.Show($"Logged in as:{Username}");
            ViewService.Instance.ShowWindow(new CalendarPageViewModel());
            Application.Current.MainWindow.Close();

        }
    }
}
